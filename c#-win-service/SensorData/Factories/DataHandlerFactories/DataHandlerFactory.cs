using Interfaces;
using ObjService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataHandlerFactories
{
    public class DataHandlerFactory
    {
        public IDataHandler GetDataHandler(Type type)
        {
            return Instantiator.GetObject(type, "Handlers.dll");
        }
    }
}
