using Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ParserService
{
    /// <summary>
    /// Provides functionality related with String parsing.
    /// </summary>
    public class STRParser : IParser
    {
        #region IParser
        /// <summary>
        /// Splits the string and selects the specified data based on the args passed.
        /// </summary>
        /// <param name="args">
        /// args[0]: The string to be parsed.
        /// args[1]: The delimeter to split the string.
        /// args[2]: The position of the splitted array to select.
        /// </param>
        /// <returns>A List that contains the value that is extracted from the string.</returns>
        public List<string> ExtractData(params object[] args)
        {
            List<string> extractedData = new List<string>();
            try
            {
                string[] strDataArray = args[0].ToString().Split(char.Parse(args[1].ToString()));
                extractedData.Add(strDataArray[int.Parse(args[2].ToString())]);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry(MethodBase.GetCurrentMethod().Name, e.ToString(), EventLogEntryType.Error);
            }
            return extractedData;
        }

        /// <summary>
        /// Assuming that valid strings will contain alphanumeric characters.
        /// Strings that are numbers are not considered to be valid strings.
        /// </summary>
        /// <param name="data">The string to check.</param>
        /// <returns>true if is a valid string, otherwise false.</returns>
        public bool ValidateData(string data)
        {
            bool isValidString = false;
            try
            {
                isValidString = int.TryParse(data, out _);
                isValidString = double.TryParse(data, out _);
            }
            catch(Exception e)
            {
                EventLog.WriteEntry(MethodBase.GetCurrentMethod().Name, e.ToString(), EventLogEntryType.Error);
            }
            return isValidString;
        }
        #endregion

        #region String parsing methods

        #endregion
    }
}
