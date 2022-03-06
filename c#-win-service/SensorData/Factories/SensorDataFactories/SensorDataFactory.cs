using DataHandlerFactories;
using Handlers;
using Interfaces;
using SensorDataController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataFactory
{
    public class SensorDataFactory
    {
        public ServiceController GetInstance()
        {
            IDataHandler sensorDataHandler = new DataHandlerFactory().GetDataHandler(typeof(SensorDataHandler));
            IDataHandler geocodeDataHandler = new DataHandlerFactory().GetDataHandler(typeof(GeocodeDataHandler));
            IDataHandler dataStorageHandler = new DataHandlerFactory().GetDataHandler(typeof(DataStorageHandler));
            return new ServiceController(sensorDataHandler, geocodeDataHandler, dataStorageHandler);
        }
    }
}
