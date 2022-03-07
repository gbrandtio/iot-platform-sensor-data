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
    public class SensorDataControllerFactory
    {
        public IController GetInstance()
        {
            return new ServiceController(GetDataHandlerObject(typeof(SensorDataHandler)), GetDataHandlerObject(typeof(GeocodeDataHandler)), GetDataHandlerObject(typeof(DataStorageHandler)));
        }

        private IDataHandler GetDataHandlerObject(Type type)
        {
            return new DataHandlerFactory().GetDataHandler(type);
        }
    }
}
