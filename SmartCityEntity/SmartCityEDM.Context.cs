﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartCityEntity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class SmartCityEntities : DbContext
    {
        public SmartCityEntities()
            : base("name=SmartCityEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Adm_ForecastWeather> Adm_ForecastWeather { get; set; }
        public DbSet<Adm_Weather> Adm_Weather { get; set; }
    
        [EdmFunction("SmartCityEntities", "fc_BaseLine")]
        public virtual IQueryable<fc_BaseLine_Result> fc_BaseLine(string paramStation, Nullable<System.DateTime> paramFrom, Nullable<System.DateTime> paramTo, string mode)
        {
            var paramStationParameter = paramStation != null ?
                new ObjectParameter("paramStation", paramStation) :
                new ObjectParameter("paramStation", typeof(string));
    
            var paramFromParameter = paramFrom.HasValue ?
                new ObjectParameter("paramFrom", paramFrom) :
                new ObjectParameter("paramFrom", typeof(System.DateTime));
    
            var paramToParameter = paramTo.HasValue ?
                new ObjectParameter("paramTo", paramTo) :
                new ObjectParameter("paramTo", typeof(System.DateTime));
    
            var modeParameter = mode != null ?
                new ObjectParameter("Mode", mode) :
                new ObjectParameter("Mode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fc_BaseLine_Result>("[SmartCityEntities].[fc_BaseLine](@paramStation, @paramFrom, @paramTo, @Mode)", paramStationParameter, paramFromParameter, paramToParameter, modeParameter);
        }
    
        [EdmFunction("SmartCityEntities", "fc_rpt_Tabular")]
        public virtual IQueryable<fc_rpt_Tabular_Result> fc_rpt_Tabular(string paramStation, Nullable<System.DateTime> paramFrom, Nullable<System.DateTime> paramTo, string mode)
        {
            var paramStationParameter = paramStation != null ?
                new ObjectParameter("paramStation", paramStation) :
                new ObjectParameter("paramStation", typeof(string));
    
            var paramFromParameter = paramFrom.HasValue ?
                new ObjectParameter("paramFrom", paramFrom) :
                new ObjectParameter("paramFrom", typeof(System.DateTime));
    
            var paramToParameter = paramTo.HasValue ?
                new ObjectParameter("paramTo", paramTo) :
                new ObjectParameter("paramTo", typeof(System.DateTime));
    
            var modeParameter = mode != null ?
                new ObjectParameter("Mode", mode) :
                new ObjectParameter("Mode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fc_rpt_Tabular_Result>("[SmartCityEntities].[fc_rpt_Tabular](@paramStation, @paramFrom, @paramTo, @Mode)", paramStationParameter, paramFromParameter, paramToParameter, modeParameter);
        }
    
        public virtual ObjectResult<sp_GetDataForColLineAndPie_Result> sp_GetDataForColLineAndPie(string paramStation, Nullable<System.DateTime> paramFrom, Nullable<System.DateTime> paramTo)
        {
            var paramStationParameter = paramStation != null ?
                new ObjectParameter("paramStation", paramStation) :
                new ObjectParameter("paramStation", typeof(string));
    
            var paramFromParameter = paramFrom.HasValue ?
                new ObjectParameter("paramFrom", paramFrom) :
                new ObjectParameter("paramFrom", typeof(System.DateTime));
    
            var paramToParameter = paramTo.HasValue ?
                new ObjectParameter("paramTo", paramTo) :
                new ObjectParameter("paramTo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetDataForColLineAndPie_Result>("sp_GetDataForColLineAndPie", paramStationParameter, paramFromParameter, paramToParameter);
        }
    
        [EdmFunction("SmartCityEntities", "fc_DualAxesLineAndColumn")]
        public virtual IQueryable<fc_DualAxesLineAndColumn_Result> fc_DualAxesLineAndColumn(string paramStation, Nullable<System.DateTime> paramFrom, Nullable<System.DateTime> paramTo, string mode)
        {
            var paramStationParameter = paramStation != null ?
                new ObjectParameter("paramStation", paramStation) :
                new ObjectParameter("paramStation", typeof(string));
    
            var paramFromParameter = paramFrom.HasValue ?
                new ObjectParameter("paramFrom", paramFrom) :
                new ObjectParameter("paramFrom", typeof(System.DateTime));
    
            var paramToParameter = paramTo.HasValue ?
                new ObjectParameter("paramTo", paramTo) :
                new ObjectParameter("paramTo", typeof(System.DateTime));
    
            var modeParameter = mode != null ?
                new ObjectParameter("Mode", mode) :
                new ObjectParameter("Mode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fc_DualAxesLineAndColumn_Result>("[SmartCityEntities].[fc_DualAxesLineAndColumn](@paramStation, @paramFrom, @paramTo, @Mode)", paramStationParameter, paramFromParameter, paramToParameter, modeParameter);
        }
    
        [EdmFunction("SmartCityEntities", "fc_PreditInput")]
        public virtual IQueryable<fc_PreditInput_Result> fc_PreditInput(Nullable<System.DateTime> paramDate, string paramStation)
        {
            var paramDateParameter = paramDate.HasValue ?
                new ObjectParameter("paramDate", paramDate) :
                new ObjectParameter("paramDate", typeof(System.DateTime));
    
            var paramStationParameter = paramStation != null ?
                new ObjectParameter("paramStation", paramStation) :
                new ObjectParameter("paramStation", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fc_PreditInput_Result>("[SmartCityEntities].[fc_PreditInput](@paramDate, @paramStation)", paramDateParameter, paramStationParameter);
        }
    
        [EdmFunction("SmartCityEntities", "fc_GetLast7DayData")]
        public virtual IQueryable<fc_GetLast7DayData_Result> fc_GetLast7DayData(string paramStation, Nullable<System.DateTime> paramFrom, Nullable<System.DateTime> paramTo)
        {
            var paramStationParameter = paramStation != null ?
                new ObjectParameter("paramStation", paramStation) :
                new ObjectParameter("paramStation", typeof(string));
    
            var paramFromParameter = paramFrom.HasValue ?
                new ObjectParameter("paramFrom", paramFrom) :
                new ObjectParameter("paramFrom", typeof(System.DateTime));
    
            var paramToParameter = paramTo.HasValue ?
                new ObjectParameter("paramTo", paramTo) :
                new ObjectParameter("paramTo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fc_GetLast7DayData_Result>("[SmartCityEntities].[fc_GetLast7DayData](@paramStation, @paramFrom, @paramTo)", paramStationParameter, paramFromParameter, paramToParameter);
        }
    }
}
