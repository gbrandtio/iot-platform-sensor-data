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
    public class MeasurementsDbDataService
    {
        MeasurementsDbContext dbContext = MeasurementsDbContext.GetInstance();

        /// <summary>
        /// Iterates through the dictionary and inserts all the different kinds of measurements into the database.
        /// </summary>
        /// <param name="measurements"></param>
        public void Insert(Dictionary<Type,List<IMeasurement>> measurements)
        {
            foreach (KeyValuePair<Type, List<IMeasurement>> keyValuePair in measurements)
            {
                dbContext.InsertMeasurement(keyValuePair.Key, keyValuePair.Value.ToList());
            }
        }
    }
}
