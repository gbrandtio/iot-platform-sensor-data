using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.DbService
{
    /// <summary>
    /// Provides a wrapper on top of the MeasurementsDbContext in order to insert the measurements
    /// into the database without caring about the details of Entity.
    /// </summary>
    public class MeasurementsDbDataService
    {
        MeasurementsDbContext dbContext = MeasurementsDbContext.GetInstance();

        /// <summary>
        /// Iterates through the dictionary and inserts all the different kinds of measurements into the database.
        /// </summary>
        /// <param name="measurements"></param>
        public void Insert(Dictionary<string,List<IMeasurement>> measurements)
        {
            foreach (KeyValuePair<string, List<IMeasurement>> keyValuePair in measurements)
            {
                dbContext.InsertMeasurement(keyValuePair.Key, keyValuePair.Value.ToList());
            }
        }
    }
}
