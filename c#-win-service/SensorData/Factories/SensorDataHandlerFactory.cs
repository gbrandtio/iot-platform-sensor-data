using Handlers;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factories
{
    public class SensorDataHandlerFactory
    {
        #region IDataHandlerFactory
        public IDataHandler GetDataHandler()
        {
            return new SensorDataHandler();
        }
        #endregion
    }
}
