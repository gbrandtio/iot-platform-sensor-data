using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    /// <summary>
    /// Defines a contract that data handlers need to implement.
    /// </summary>
    public interface IDataHandler
    {
        /// <summary>
        /// Performs operations on the passed data.
        /// </summary>
        /// <param name="dictionary">The data that need to be handled</param>
        /// <returns>The transformed data.</returns>
        Dictionary<string, List<IMeasurement>> HandleData(Dictionary<string, List<IMeasurement>> dictionary);
    }
}
