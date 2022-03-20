using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    /// <summary>
    /// Defines a contract that parsers need to implement.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Parses and extracts specific information from the passed data.
        /// </summary>
        /// <param name="args">Information about the data to be extracted, the actual data and specific fields to parse.</param>
        /// <returns>The extracted data.</returns>
        List<string> ExtractData(params object[] args);

        /// <summary>
        /// Validates that the passed data are of the expected format.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool ValidateData(string data);
    }
}
