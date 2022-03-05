using Interfaces;
using Models;
using Services.FileClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.DbClient
{
    public class MeasurementsDbDataService
    {
        public void Insert(Dictionary<Type,List<IMeasurement>> measurements)
        {
            MeasurementsDbContext dbContext = new MeasurementsDbContext();
            foreach (KeyValuePair<Type, List<IMeasurement>> keyValuePair in measurements)
            {
                if (keyValuePair.GetType() == typeof(Humidity)) dbContext.Humidity.AddRange(keyValuePair.Value.OfType<Humidity>().ToList());
                if (keyValuePair.GetType() == typeof(P1)) dbContext.P1.AddRange(keyValuePair.Value.OfType<P1>().ToList());
                if (keyValuePair.GetType() == typeof(P2)) dbContext.P2.AddRange(keyValuePair.Value.OfType<P2>().ToList());
                if (keyValuePair.GetType() == typeof(Pressure)) dbContext.Pressure.AddRange(keyValuePair.Value.OfType<Pressure>().ToList());
                if (keyValuePair.GetType() == typeof(Temperature)) dbContext.Temperature.AddRange(keyValuePair.Value.OfType<Temperature>().ToList());
            }
        }
    }
}
