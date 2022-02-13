using Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers
{
    public class GeocodeResponseParser
    {
        /// <summary>
        /// Parses the JSON Geocode API response and extracts the formatted_address field value.
        /// </summary>
        /// <param name="response">Geocode API response</param>
        /// <returns>The formatted_address value as a string</returns>
        public static string GeocodeRes_ExtractFormattedAddress(string response)
        {
            string formattedAddress = String.Empty;
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

        /// <summary>
        /// Tries to parse the formatted address string retrieved from the Geocode API and extract useful information.
        /// </summary>
        /// <param name="formattedAddress">The formatted address as retrieved from Geocode API</param>
        /// <param name="pos">The array position of the info to extract. Currently supported 1=CITY, 2=COUNTRY</param>
        /// <returns></returns>
        public static string GeocodeRes_ExtractFormattedAddressInfo(string formattedAddress, int pos)
        {
            string requestedAddressComponent = SharedValues.UNKNOWN;
            try
            {
                string[] addrArray = formattedAddress.Split(',');
                requestedAddressComponent = addrArray[pos];
            }
            catch (Exception e)
            {

            }
            return requestedAddressComponent;
        }
    }
}
