using Constants;
using Interfaces;
using MeasurementFactories;
using Models;
using Newtonsoft.Json.Linq;
using ObjService;
using ParserService;
using RestClient;
using RestService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Handlers
{
    /// <summary>
    /// Provides functionality to retrieve the sensor data, parse them and extract the useful information.
    /// </summary>
    public class SensorDataHandler : IDataHandler
    {
        #region Properties
        /// <summary>
        /// Measurements of type Humidity.
        /// </summary>
        public List<IMeasurement> HumidityData { get; set; }

        /// <summary>
        /// Measurements of type P1.
        /// </summary>
        public List<IMeasurement> P1Data { get; set; }

        /// <summary>
        /// Measurements of type P2.
        /// </summary>
        public List<IMeasurement> P2Data { get; set; }

        /// <summary>
        /// Measurements of type Pressure.
        /// </summary>
        public List<IMeasurement> PressureData { get; set; }

        /// <summary>
        /// Measurements of type Temperature.
        /// </summary>
        public List<IMeasurement> TemperatureData { get; set; }
        #endregion

        #region IDataHandler
        /// <summary>
        /// Controls the flow of retrieving the sensor data, parsing them and storing them
        /// into a list.
        /// </summary>
        /// <param name="unused">Argument in order to fulfil the IDataHandler interface.</param>
        /// <returns></returns>
        public Dictionary<string, List<IMeasurement>> HandleData(Dictionary<string, List<IMeasurement>> unused)
        {
            Dictionary<string, List<IMeasurement>> constructedDictionary = new Dictionary<string, List<IMeasurement>>();
            try
            {
                string sensorDataResponse = RetrieveSensorDataValues();
                List<IMeasurement> allMeasurements = ExtractSensorDataValues(sensorDataResponse);
                constructedDictionary = GetSeparatedMeasurementLists(allMeasurements);
            }
            catch(Exception e)
            {
                EventLog.WriteEntry(MethodBase.GetCurrentMethod().Name, e.ToString(), EventLogEntryType.Error);
            }

            return constructedDictionary;
        }
        #endregion

        #region Data Extractor Methods
        /// <summary>
        /// Performs a GET request to the configured API in order to retrieve
        /// the sensor data.
        /// </summary>
        /// <returns>The API response.</returns>
        public string RetrieveSensorDataValues()
        {
            LogHandler.Log(new Log(MethodBase.GetCurrentMethod().Name, Strings.Sensor.SensorApi.Value + Strings.Config.CountryCode.Value, Severity.Info));
            string response = GET.DoRequest(Strings.Sensor.SensorApi.Value + Strings.Config.CountryCode.Value);
            LogHandler.Log(new Log(MethodBase.GetCurrentMethod().Name, "Sensor API response: " + response, Severity.Info));
            return response;
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
                    Dictionary<string, double> measurementPairs = ExtractMeasurementJSONObjects(obj);
                    // Extract sensor data values
                    foreach (KeyValuePair<string, double> pair in measurementPairs)
                    {
                        // Dynamically instantiate concrete object of IMeasurement.
                        IMeasurement measurement = MeasurementsFactory.GetInstance(pair.Key, pair.Value, location);
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

        /// <summary>
        /// Parses a JSON object and returns the location information.
        /// </summary>
        /// <param name="obj">The JSON object to parse</param>
        /// <returns>The ILocation object or null</returns>
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
            Dictionary<string, string> extractedPairs = jsonParser.ExtractJMeasurementsFromEachJObject(jArraySensorDataValues, Strings.Sensor.ValueType.Value, Strings.String.ValueR.Value);
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
        public Dictionary<string, List<IMeasurement>> GetSeparatedMeasurementLists(List<IMeasurement> allMeasurements)
        {
            Dictionary<string, List<IMeasurement>> dicSeparatedLists = new Dictionary<string, List<IMeasurement>>();
            try
            {
                // Find all measurement names.
                Stack<string> types = new Stack<string>();
                foreach (IMeasurement measurement in allMeasurements)
                {
                    if (measurement != null && !types.Contains(measurement.Name))
                    {
                        types.Push(measurement.Name);
                    }
                }

                // Construct each list.
                foreach (string type in types)
                {
                    dicSeparatedLists.Add(type, GetSpecificMeasurementObjectList(allMeasurements, type));
                }
            }
            catch (Exception e)
            {
                EventLog.WriteEntry(MethodBase.GetCurrentMethod().Name, e.ToString(), EventLogEntryType.Error);
            }
            return dicSeparatedLists;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Given a type, returns all the objects inside a list that are a concrete implementation of this type.
        /// </summary>
        /// <param name="measurements">List of measurements to get objects from.</param>
        /// <param name="measurementName">Type of objects to add to the return list.</param>
        /// <returns>List of objects with the specified type</returns>
        private List<IMeasurement> GetSpecificMeasurementObjectList(List<IMeasurement> measurements, string measurementName)
        {
            List<IMeasurement> specificMeasurementObjectList = new List<IMeasurement>();
            foreach (IMeasurement measurement in measurements)
            {
                if (measurement != null)
                    if (measurementName == measurement.Name) specificMeasurementObjectList.Add(measurement);
            }
            return specificMeasurementObjectList;
        }
        #endregion
    }
}
