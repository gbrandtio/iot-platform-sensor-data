using Models;
using Services.FileService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestService
{
    /// <summary>
    /// This class is responsible for retrieving sensor data from the configured API.
    /// </summary>
    public class GET
    {
        #region Http Request Methods
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
            catch (Exception e)
            {
            }
            return response;
        }
        #endregion
    }
}