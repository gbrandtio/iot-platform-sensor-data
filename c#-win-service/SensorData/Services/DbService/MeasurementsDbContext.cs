using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DbService
{
    public class MeasurementsDbContext : DbContext
    {
        private static MeasurementsDbContext instance = null;
        private MeasurementsDbContext() : base("MsrmntsDBConnectionString")
        {
            Database.SetInitializer<MeasurementsDbContext>(new MeasurementsDbInitializer());
        }

        public static MeasurementsDbContext GetInstance()
        {
            if (instance == null) instance = new MeasurementsDbContext();
            return instance;
        }

        public void InsertMeasurement(Type type, List<IMeasurement> list)
        {
            if (type == typeof(Humidity)) this.Humidity.AddRange(list.OfType<Humidity>().ToList());
            if (type == typeof(P1)) this.P1.AddRange(list.OfType<P1>().ToList());
            if (type == typeof(P2)) this.P2.AddRange(list.OfType<P2>().ToList());
            if (type == typeof(Pressure)) this.Pressure.AddRange(list.OfType<Pressure>().ToList());
            if (type == typeof(Temperature)) this.Temperature.AddRange(list.OfType<Temperature>().ToList());
        }

        public DbSet<Humidity> Humidity { get; set; }
        public DbSet<P1> P1 { get; set; }
        public DbSet<P2> P2 { get; set; }
        public DbSet<Pressure> Pressure { get; set; }
        public DbSet<Temperature> Temperature { get; set; }
    }
}
