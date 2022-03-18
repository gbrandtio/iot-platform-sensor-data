using Constants;
using Interfaces;
using Models;
using Newtonsoft.Json.Linq;
using ObjService;
using ParserService;
using RestClient;
using RestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Handlers
{
    public class SensorDataHandler : IDataHandler
    {
        #region Properties
        public List<IMeasurement> HumidityData { get; set; }
        public List<IMeasurement> P1Data { get; set; }
        public List<IMeasurement> P2Data { get; set; }
        public List<IMeasurement> PressureData { get; set; }
        public List<IMeasurement> Temperaturedata { get; set; }
        #endregion

        #region IDataHandler
        public Dictionary<Type, List<IMeasurement>> HandleData(Dictionary<Type, List<IMeasurement>> unused)
        {
            string sensorDataResponse = RetrieveSensorDataValues();
            List<IMeasurement> allMeasurements = ExtractSensorDataValues(sensorDataResponse);
            return GetSeparatedMeasurementLists(allMeasurements);
        }
        #endregion

        #region Data Extractor Methods
        public string RetrieveSensorDataValues()
        {
            LogHandler.Log(new Log(MethodBase.GetCurrentMethod().Name, Strings.Sensor.SensorApi.Value + Strings.Config.CountryCode.Value, Severity.Info));
            return GET.DoRequest(Strings.Sensor.SensorApi.Value + Strings.Config.CountryCode.Value);
        }

        /// <summary>
        /// Iterates through the json response of the sensor API and tries to extract the required measurements
        /// and the location of each sensor.
        /// </summary>
        /// <param name="json">The json response from the Geocode API.</param>
        /// <returns>A List of IMeasurement objects. It is possible for a member of the List to be NULL.</returns>
        public List<IMeasurement> ExtractSensorDataValues(string json)
        {
            List<IMeasurement> measurements = new List<IMeasurement>();
            try
            {
                JArray jResultsArray = JArray.Parse(json);
                foreach (JObject obj in jResultsArray)
                {
                    ILocation location = ExtractLocationInfo(obj);
                    // Extract sensor data values
                    foreach (KeyValuePair<string, double> pair in ExtractMeasurementJSONObjects(obj))
                    {
                        // Dynamically instantiate concrete object of IMeasurement.
                        IMeasurement measurement = Instantiator.GetObject(pair.Key, "Models.dll", pair.Value, location);
                        measurements.Add(measurement);
                    }
                }
            }
            catch (Exception e)
            {
                LogHandler.Log(new Log(MethodBase.GetCurrentMethod().Name, e.ToString(), Severity.Exception));
            }
            return measurements;
        }

        private ILocation ExtractLocationInfo(JObject obj)
        {
            double longitude, latitude;
            string country;
            ILocation location = null;

            // Extract location.
            try
            {
                JSONParser jsonParser = new JSONParser();
                JToken jLocation = (JObject)obj.GetValue(Strings.String.Location.Value);
                country = jsonParser.ExtractJPropertyFromJObject(jLocation, Strings.String.Country.Value);
                double.TryParse(jsonParser.ExtractJPropertyFromJObject(jLocation, Strings.String.Longitude.Value), out longitude);
                double.TryParse(jsonParser.ExtractJPropertyFromJObject(jLocation, Strings.String.Longitude.Value), out latitude);
                
                location = new Location(longitude, latitude, country);
            }
            catch (Exception e)
            {
                LogHandler.Log(new Log(MethodBase.GetCurrentMethod().Name, e.ToString(), Severity.Exception));
            }

            return location;
        }
        
        /// <summary>
        /// Constructs a dictionary with the measurement name and value and returns it.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Dictionary<string, double> ExtractMeasurementJSONObjects(JObject obj)
        {
            JArray jArraySensorDataValues = (JArray)obj.GetValue(Strings.Sensor.SensorDataValues.Value);
            JSONParser jsonParser = new JSONParser();
            Dictionary<string, string> extractedPairs = jsonParser.ExtractJMeasurementsFromEachJObject(jArraySensorDataValues);
            Dictionary<string, double> convertedPairs = new Dictionary<string, double>();
            foreach (KeyValuePair<string, string> pair in extractedPairs)
            {
                convertedPairs.Add(pair.Key, double.Parse(pair.Value));
            }
            return convertedPairs;
        }
        #endregion

        #region Data Handling Methods
        /// <summary>
        /// Constructs a dictionary that contains lists of objects for all IMeasurement concrete implementations.
        /// </summary>
        /// <param name="allMeasurements">List with different concrete objects of IMeasurement interface.</param>
        /// <returns>Dictionary of a concrete IMeasrement implementation.</returns>
        public Dictionary<Type, List<IMeasurement>> GetSeparatedMeasurementLists(List<IMeasurement> allMeasurements)
        {
            // Construct each list
            List<Type> types = Instantiator.GetTypes("Models.dll");
            Dictionary<Type, List<IMeasurement>> dicSeparatedLists = new Dictionary<Type, List<IMeasurement>>();
            foreach (Type type in types)
            {
                dicSeparatedLists.Add(type, GetSpecificMeasurementObjectList(allMeasurements, type));
            }
            return dicSeparatedLists;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Given a type, returns all the objects inside a list that are a concrete implementation of this type.
        /// </summary>
        /// <param name="measurements">List of measurements to get objects from.</param>
        /// <param name="type">Type of objects to add to the return list.</param>
        /// <returns>List of objects with the specified type</returns>
        private List<IMeasurement> GetSpecificMeasurementObjectList(List<IMeasurement> measurements, Type type)
        {
            List<IMeasurement> specificMeasurementObjectList = new List<IMeasurement>();
            foreach (IMeasurement measurement in measurements)
            {
                if (measurement != null)
                    if (type == measurement.GetType()) specificMeasurementObjectList.Add(measurement);
            }
            return specificMeasurementObjectList;
        }
        #endregion
    }
}
