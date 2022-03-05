using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDataHandler
    {
        Dictionary<Type, List<IMeasurement>> HandleData(Dictionary<Type, List<IMeasurement>> dictionary);
    }
}
