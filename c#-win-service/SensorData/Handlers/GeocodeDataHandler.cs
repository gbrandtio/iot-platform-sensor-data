using Constants;
using Interfaces;
using Models;
using Newtonsoft.Json.Linq;
using RestClient;
using RestService;
using ServiceFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Handlers
{
    /// <summary>
    /// Provides functionality to retrieve the location data of a measurement,
    /// parse them and extract useful information.
    /// </summary>
    public class GeocodeDataHandler : IDataHandler
    {
        #region Members
        private IParser parser;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GeocodeDataHandler() { }
        #endregion

        #region IDataHandler
        /// <summary>
        /// Loops through all the data provided, retrieves the location info for each measurement and 
        /// adds the information to the respective object.
        /// </summary>
        /// <param name="allMeasurements">Dictionary with all the measurements in separate lists.</param>
        /// <returns></returns>
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

        #region Reverse Geocoding
        /// <summary>
        /// Retrieves the location info based on the passed coordinates and extracts the city from the response.
        /// </summary>
        /// <param name="longitude">Longitude of the measurement.</param>
        /// <param name="latitude">Latitude of the measurement.</param>
        /// <returns>The city that matches the passed coordinates.</returns>
        private string FindLocationInfo(double longitude, double latitude)
        {
            string localeInfo = Strings.String.Unknown.Value;
            try
            {
                string response = RetrieveGeocodeResponse(longitude, latitude);
                string formattedAddress = ExtractInfo(GetParser(response), response, Strings.String.Results.Value, Strings.Geocode.FormattedAddress.Value)[0];
                localeInfo = ExtractInfo(GetParser(formattedAddress), formattedAddress, Strings.Geocode.InfoCity.ValueInt, ',')[0];
            }
            catch (Exception e)
            {
                LogHandler.Log(new Log(MethodBase.GetCurrentMethod().Name, e.ToString(), Severity.Exception));
            }
            return localeInfo;
        }
        #endregion

        #region Services Interaction Methods
        /// <summary>
        /// Performs a GET request to the Geocode API.
        /// </summary>
        /// <param name="longitude">Longitude of the measurement.</param>
        /// <param name="latitude">Latitude of the measurement.</param>
        /// <returns></returns>
        private string RetrieveGeocodeResponse(double longitude, double latitude)
        {
            string URL = Strings.Geocode.GeocodeURL.Value + longitude + "," + latitude + Strings.Geocode.URLKeyParam.Value + Strings.Config.GeoApiKey.Value;
            return GET.DoRequest(URL);
        }

        /// <summary>
        /// Returns a parser instance based on the format of the data passed.
        /// </summary>
        /// <param name="data">The data to be parsed.</param>
        /// <returns>The correct parser instance.</returns>
        private IParser GetParser(string data)
        {
            ParserServiceFactory parserServiceFactory = new ParserServiceFactory();
            parser = parserServiceFactory.GetInstance(data);
            return parser;
        }

        /// <summary>
        /// Uses the passed parser and arguments in order to parse the data and extract information
        /// based on the provided flags.
        /// For the flags that need to be passed see the concrete implementations of each parser.
        /// </summary>
        /// <param name="parser">The parser to parse the data.</param>
        /// <param name="args">The arguments that correspond to the parser. See JSONParser, STRParser, XMLParser</param>
        /// <returns></returns>
        private List<string> ExtractInfo(IParser parser, params object[] args)
        {
            return parser.ExtractData(args);
        }
        #endregion
    }
}
