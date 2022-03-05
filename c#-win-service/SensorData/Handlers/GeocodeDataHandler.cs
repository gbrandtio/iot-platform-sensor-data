using Constants;
using Interfaces;
using Models;
using Newtonsoft.Json.Linq;
using RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Handlers
{
    public class GeocodeDataHandler : IDataHandler
    {
        #region Constructor
        public GeocodeDataHandler() { }
        #endregion

        #region Data Handling Methods
        public Dictionary<Type, List<IMeasurement>> HandleData(Dictionary<Type, List<IMeasurement>> allMeasurements)
        {
            foreach (KeyValuePair<Type, List<IMeasurement>> entry in allMeasurements)
            {
                Type measurementType = entry.Key;
                List<IMeasurement> measurements = entry.Value;
                foreach(IMeasurement measurement in measurements)
                {
                    if (measurement != null)
                        measurement.Location.City = FindLocationInfo(measurement.Location.Longitude, measurement.Location.Latitude);
                }
            }
            return allMeasurements;
        }
        #endregion

        #region Parsing Methods
        /// <summary>
        /// Parses the Geocode API JSON response and extracts the specified info from the formatted_address field.
        /// </summary>
        /// <param name="json">The Geocode JSON API response</param>
        /// <returns>The requested info from the formatted address</returns>
        private string ExtractData(string json, int pos, char delimeter) 
        {
            string formattedAddress = ExtractFormattedAddress(json);
            string requestedAddressComponent = Strings.String.Unknown.Value;
            try
            {
                string[] addrArray = formattedAddress.Split(delimeter);
                requestedAddressComponent = addrArray[pos];
            }
            catch (Exception e)
            {
                LogHandler.Log(new Log(MethodBase.GetCurrentMethod().Name, e.ToString(), Severity.Exception));
            }
            return requestedAddressComponent;
        }

        /// <summary>
        /// Parses the JSON Geocode API response and extracts the formatted_address field value.
        /// </summary>
        /// <param name="response">Geocode API response</param>
        /// <returns>The formatted_address value as a string</returns>
        private string ExtractFormattedAddress(string response)
        {
            string formattedAddress = Strings.String.Unknown.Value;
            try
            {
                JObject jResponse = JObject.Parse(response);
                JArray jResultsArray = (JArray)jResponse[Strings.String.Results.Value];
                formattedAddress = jResultsArray[0][Strings.Geocode.FormattedAddress.Value].ToString();
            }
            catch (Exception e)
            {
                LogHandler.Log(new Log(MethodBase.GetCurrentMethod().Name, e.ToString(), Severity.Exception));
            }
            return formattedAddress;
        }
        #endregion

        #region Reverse Geocoding
        private string FindLocationInfo(double longitude, double latitude)
        {
            string localeInfo = Strings.String.Unknown.Value;
            try
            {
                string URL = Strings.Geocode.GeocodeURL.Value + longitude + "," + latitude + Strings.Geocode.URLKeyParam + Strings.Config.GeoApiKey;
                string response = GET.DoRequest(URL);

                localeInfo = ExtractData(response, Strings.Geocode.InfoCity.ValueInt, ',');
                if (localeInfo.Equals(Strings.String.Unknown.Value)) localeInfo = ExtractData(response, Strings.Geocode.InfoCountry.ValueInt, ',');
            }
            catch (Exception e)
            {
                LogHandler.Log(new Log(MethodBase.GetCurrentMethod().Name, e.ToString(), Severity.Exception));
            }
            return localeInfo;
        }
        #endregion
    }
}
