using Handlers;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Factories
{
    public class DataStorageHandlerFactory : IDataHandlerFactory
    {
        #region IDataHandler
        public IDataHandler GetDataHandler()
        {
            return new DataStorageHandler();
        }
        #endregion
    }
}
