using Handlers;
using Interfaces;
using ObjService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataHandlerFactories
{
    /// <summary>
    /// Provides functionality of concrete object construction.
    /// </summary>
    public class DataHandlerFactory
    {
        /// <summary>
        /// Creates a concrete data handler object based on the passed type.
        /// </summary>
        /// <param name="type">The data handler object type to create.</param>
        /// <returns>A concrete data handler object.</returns>
        public IDataHandler GetDataHandler(Type type)
        {
            EventLog.WriteEntry(MethodBase.GetCurrentMethod().Name, "Object Type: " + type, EventLogEntryType.Information);
            if (type == typeof(SensorDataHandler)) return new SensorDataHandler();
            if (type == typeof(GeocodeDataHandler)) return new GeocodeDataHandler();
            if (type == typeof(DataStorageHandler)) return new DataStorageHandler();
            return null;
        }
    }
}
