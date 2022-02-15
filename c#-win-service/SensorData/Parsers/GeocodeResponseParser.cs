using Helpers;
using Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers
{
    public class GeocodeResponseParser : IParser
    {
        public string ExtractData(string json) 
        {
            string formattedAddress = GeocodeRes_ExtractFormattedAddress(json);
            return formattedAddress;
        }


        /// <summary>
        /// Tries to parse the formatted address string retrieved from the Geocode API and extract useful information.
        /// </summary>
        /// <param name="formattedAddress">The formatted address as retrieved from Geocode API</param>
        /// <param name="pos">The array position of the info to extract. Currently supported 1=CITY, 2=COUNTRY</param>
        /// <returns></returns>
        public string ExtractSpecificInfo(string formattedAddress, int pos, char delimeter)
        {
            string requestedAddressComponent = SharedValues.UNKNOWN;
            try
            {
                string[] addrArray = formattedAddress.Split(delimeter);
                requestedAddressComponent = addrArray[pos];
            }
            catch (Exception e)
            {

            }
            return requestedAddressComponent;
        }

        #region Private Methods
        /// <summary>
        /// Parses the JSON Geocode API response and extracts the formatted_address field value.
        /// </summary>
        /// <param name="response">Geocode API response</param>
        /// <returns>The formatted_address value as a string</returns>
        private string GeocodeRes_ExtractFormattedAddress(string response)
        {
            string formattedAddress = SharedValues.UNKNOWN;
            try
            {
                JObject jResponse = JObject.Parse(response);
                JArray jResultsArray = (JArray)jResponse[SharedValues.ARRAY_RESULTS];
                formattedAddress = jResultsArray[0][SharedValues.FORMATTED_ADDRESS].ToString();
            }
            catch (Exception e)
            {

            }
            return formattedAddress;
        }
        #endregion
    }
}
