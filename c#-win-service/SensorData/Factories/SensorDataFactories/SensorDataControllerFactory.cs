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
    /// <summary>
    /// Responsible for constructing the correct ServiceController instance.
    /// </summary>
    public class SensorDataControllerFactory
    {
        /// <summary>
        /// Constructs a service controller instance by getting instances of the correct data handlers.
        /// </summary>
        /// <returns>A ServiceController instance.</returns>
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
