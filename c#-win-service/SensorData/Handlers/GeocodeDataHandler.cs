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
    public class GeocodeDataHandler : IDataHandler
    {
        #region Members
        private IParser parser;
        #endregion

        #region Constructor
        public GeocodeDataHandler() { }
        #endregion

        #region IDataHandler
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
        private string RetrieveGeocodeResponse(double longitude, double latitude)
        {
            string URL = Strings.Geocode.GeocodeURL.Value + longitude + "," + latitude + Strings.Geocode.URLKeyParam.Value + Strings.Config.GeoApiKey.Value;
            return GET.DoRequest(URL);
        }

        private IParser GetParser(string data)
        {
            ParserServiceFactory parserServiceFactory = new ParserServiceFactory();
            parser = parserServiceFactory.GetInstance(data);
            return parser;
        }

        private List<string> ExtractInfo(IParser parser, params object[] args)
        {
            return parser.ExtractData(args);
        }
        #endregion
    }
}
