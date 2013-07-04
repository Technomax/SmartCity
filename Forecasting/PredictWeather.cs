using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NNHelpers.Activation;
using NNHelpers.Feedforward;
using NNHelpers.Feedforward.Train;
using NNHelpers.Feedforward.Train.Anneal;
using NNHelpers.Feedforward.Train.Backpropagation;
using NNHelpers.Util;

namespace Forecasting
{
    public class PredictWeather
    {
        public const int TRAINING_SIZE = 30000;
        public const int INPUT_SIZE = 10;
        public const int OUTPUT_SIZE = 1;
        public const int NEURONS_HIDDEN_1 = 20;
        public const int NEURONS_HIDDEN_2 = 0;
        public const double MAX_ERROR = 13.2;
        public DateTime PREDICT_FROM = ReadCSV.ParseDate("2010-04-19");
        public DateTime LEARN_FROM = ReadCSV.ParseDate("2003-04-19");

        static void Main(string[] args)
        {
            PredictWeather predict = new PredictWeather();
            //set true to train
                predict.run(true);
        }

        private double[][] input;

        private double[][] ideal;

        private FeedforwardNetwork network;

        private WeatherActual actual;

        public void createNetwork()
        {
            ActivationFunction threshold = new ActivationTANH();
            this.network = new FeedforwardNetwork();
            this.network.AddLayer(new FeedforwardLayer(threshold, PredictWeather.INPUT_SIZE * 4));
            this.network.AddLayer(new FeedforwardLayer(threshold, PredictWeather.NEURONS_HIDDEN_1));
            if (PredictWeather.NEURONS_HIDDEN_2 > 0)
            {
                this.network.AddLayer(new FeedforwardLayer(threshold, PredictWeather.NEURONS_HIDDEN_2));
            }
            this.network.AddLayer(new FeedforwardLayer(threshold, PredictWeather.OUTPUT_SIZE*3));
            this.network.Reset();
        }

        //pass data to other from here.
        public void display()
        {

            double[] present = new double[INPUT_SIZE * 4];
            double[] predict = new double[OUTPUT_SIZE * 3];
            double[] actualOutput = new double[OUTPUT_SIZE * 3];

            int index = 0;
            foreach (WeatherSamples sample in this.actual.getSamples())
            {
                if (sample.getDate().CompareTo(this.PREDICT_FROM) > 0)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append(ReadCSV.DisplayDate(sample.getDate()));
                    str.Append(":Start=");
                    //str.Append(sample.airPollutionLevel);
                    //str.Append(sample.minTemp);
                    //str.Append(sample.maxTemp);
                    //str.Append(sample.rainfall);

                    this.actual.getInputData(index - INPUT_SIZE, present);
                     //DateTime date= ReadCSV.ParseDate("2010-04-19");
                     //this.actual.getInputDataToPredict(date,present);
                    this.actual.getOutputData(index - INPUT_SIZE, actualOutput);

                    predict = this.network.ComputeOutputs(present);
                    str.Append(",Actual minTemp =");
                    str.Append(actualOutput[0].ToString("N2"));
                    str.Append(",Actual maxTemp =");
                    str.Append(actualOutput[1].ToString("N2"));
                    str.Append(",Actual rainfall =");
                    str.Append(actualOutput[2].ToString("N2"));
                    str.Append(",Predicted minTemp= ");
                    str.Append(predict[0].ToString("N2"));
                    str.Append(",Predicted maxTemp= ");
                    str.Append(predict[1].ToString("N2"));
                    str.Append(",Predicted rainfall= ");
                    str.Append(predict[2].ToString("N2"));

                    str.Append(":Difference=");

                    ErrorCalculation error = new ErrorCalculation();
                    error.UpdateError(predict, actualOutput);
                    str.Append(error.CalculateRMS().ToString("N2"));

                    // 

                    Console.WriteLine(str.ToString());
                }

                index++;
            }
            
        }

        public List<WeatherSamples> prediction(DateTime date,string station, double airPollutionLevel)
        {
            this.actual = new WeatherActual(INPUT_SIZE, OUTPUT_SIZE);
            loadNeuralNetwork(station);
            List<WeatherSamples> list = new List<WeatherSamples>();
            List<double[]> predictHistory = new List<double[]>();
            double[] present = new double[INPUT_SIZE * 4];
            double[] predict = new double[OUTPUT_SIZE * 3];
            this.actual.getInputDataToPredict(date, present);
            var tempDate = date;
            for (int i = 0; i < 30; i++)
            {
                predict = this.network.ComputeOutputs(present);
                predictHistory.Add(predict);
                this.actual.getInputDataToPredictSeries(date, present,predictHistory,airPollutionLevel);
                list.Add(new WeatherSamples {date=tempDate,minTemp=predict[0],maxTemp=predict[1],rainfall=predict[2],airPollutionLevel=airPollutionLevel,station=station });
                tempDate=tempDate.AddDays(+1);

            }
            return list;
        }

        private void generateTrainingSets()
        {
            this.input = new double[TRAINING_SIZE][];//[INPUT_SIZE * 2];
            this.ideal = new double[TRAINING_SIZE][];//[OUTPUT_SIZE];

            // find where we are starting from
            int startIndex = 0;
            foreach (WeatherSamples sample in this.actual.getSamples())
            {
                if (sample.getDate().CompareTo(this.LEARN_FROM) > 0)
                {
                    break;
                }
                startIndex++;
            }

            // create a sample factor across the training area
            int eligibleSamples = TRAINING_SIZE - startIndex;
            if (eligibleSamples == 0)
            {
                Console.WriteLine("Need an earlier date for LEARN_FROM or a smaller number for TRAINING_SIZE.");
                return;
            }
            int factor = eligibleSamples / TRAINING_SIZE;

            // grab the actual training data from that point
            for (int i = 0; i < TRAINING_SIZE; i++)
            {
                this.input[i] = new double[INPUT_SIZE * 4];
                this.ideal[i] = new double[OUTPUT_SIZE*3];
                this.actual.getInputData(startIndex + (i * factor), this.input[i]);
                this.actual.getOutputData(startIndex + (i * factor), this.ideal[i]);
            }
        }

        public void loadNeuralNetwork(string station)
        {
            this.network = (FeedforwardNetwork)SerializeObject.Load(station+"_weather.net");
        }

        public void saveNeuralNetwork(string station)
        {
            SerializeObject.Save(station+"_weather.net", this.network);
        }

        public void run(bool full)
        {
           
            this.actual = new WeatherActual(INPUT_SIZE, OUTPUT_SIZE);

            //loads data from database
            this.actual.load();

           // Console.WriteLine("Samples read: " + this.actual.size());

            if (full)
            {
                createNetwork();
                generateTrainingSets();
                trainNetworkBackprop();
                saveNeuralNetwork("Kathmandu");
            }
           else
            {
                loadNeuralNetwork("Kathmandu");
            }
            //call the process here to trigger data to visualiztion
            //display();
          
      
        }

        private void trainNetworkBackprop()
        {
            Train train = new Backpropagation(this.network, this.input, this.ideal, 0.000001, 0.1);
            double lastError = Double.MaxValue;
            int epoch = 1;
            int lastAnneal = 0;

            do
            {
                train.Iteration();
                double error = train.Error;

                Console.WriteLine("Iteration(Backprop) #" + epoch + " Error:"+ error);

                if (error > 0.05)
                {
                    if ((lastAnneal > 10) && (error > lastError || Math.Abs(error - lastError) < 0.0001))
                    {
                        trainNetworkAnneal();
                        lastAnneal = 0;
                    }
                }

                lastError = train.Error;
                epoch++;
                lastAnneal++;
            } while (train.Error > MAX_ERROR);
        }

        private void trainNetworkAnneal()
        {
            Console.WriteLine("Training with simulated annealing for 5 iterations");
            // train the neural network
            NeuralSimulatedAnnealing train = new NeuralSimulatedAnnealing(
                   this.network, this.input, this.ideal, 10, 2, 100);

            int epoch = 1;

            for (int i = 1; i <= 5; i++)
            {
                train.Iteration();
                Console.WriteLine("Iteration(Anneal) #" + epoch + " Error:"
                        + train.Error);
                epoch++;
            }
        }

        public void trainStation(string station) {
            this.actual = new WeatherActual(INPUT_SIZE, OUTPUT_SIZE);
            this.actual.load(station);
            createNetwork();
            generateTrainingSets();
            trainNetworkBackprop();
            saveNeuralNetwork(station);
     
        }
       
    }
}
