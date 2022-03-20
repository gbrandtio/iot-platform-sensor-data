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
    /// <summary>
    /// Provides functionality to represent and insert values in the database tables.
    /// </summary>
    public class MeasurementsDbContext : DbContext
    {
        private static MeasurementsDbContext instance = null;
        private MeasurementsDbContext() : base("MsrmntsDBConnectionString")
        {
            Database.SetInitializer<MeasurementsDbContext>(new MeasurementsDbInitializer());
        }

        /// <summary>
        /// Returns the current instance of the class or creates a new one - singleton pattern.
        /// </summary>
        /// <returns></returns>
        public static MeasurementsDbContext GetInstance()
        {
            if (instance == null) instance = new MeasurementsDbContext();
            return instance;
        }

        /// <summary>
        /// Inserts the passed data to the appropriate list.
        /// </summary>
        /// <param name="type">The type of the measurement to be inserted.</param>
        /// <param name="list">The list that contains the data.</param>
        public void InsertMeasurement(Type type, List<IMeasurement> list)
        {
            if (type == typeof(Humidity)) this.Humidity.AddRange(list.OfType<Humidity>().ToList());
            if (type == typeof(P1)) this.P1.AddRange(list.OfType<P1>().ToList());
            if (type == typeof(P2)) this.P2.AddRange(list.OfType<P2>().ToList());
            if (type == typeof(Pressure)) this.Pressure.AddRange(list.OfType<Pressure>().ToList());
            if (type == typeof(Temperature)) this.Temperature.AddRange(list.OfType<Temperature>().ToList());
        }

        /// <summary>
        /// Humidity table.
        /// </summary>
        public DbSet<Humidity> Humidity { get; set; }
        /// <summary>
        /// P1 table.
        /// </summary>
        public DbSet<P1> P1 { get; set; }
        /// <summary>
        /// P2 table.
        /// </summary>
        public DbSet<P2> P2 { get; set; }
        /// <summary>
        /// Pressure table.
        /// </summary>
        public DbSet<Pressure> Pressure { get; set; }
        /// <summary>
        /// Temperature table.
        /// </summary>
        public DbSet<Temperature> Temperature { get; set; }
    }
}
