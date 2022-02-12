using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestClient
{
    /// <summary>
    /// This class is responsible for retrieving sensor data from the configured API.
    /// </summary>
    public class GET
    {
        public static string DoRequest(string url)
        {
            string response = String.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse())
                using (Stream stream = httpResponse.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    response = reader.ReadToEnd();
                }
            }
            catch(Exception e)
            {

            }
            return response;
        }

        public static string GeocodeRes_ExtractFormattedAddress(string response)
        {
            string formattedAddress = String.Empty;
            try
            {
                JObject joResponse = JObject.Parse(response);
                JArray ojObject = (JArray)joResponse["results"];
                ojObject[0]["formatted_address"].ToString();
            }
            catch(Exception e)
            {

            }
            return formattedAddress;
        }
    }
}
