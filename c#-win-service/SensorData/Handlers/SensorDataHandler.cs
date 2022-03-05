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
    public class SensorDataHandler
    {
        #region Properties
        public List<IMeasurement> HumidityData { get; set; }
        public List<IMeasurement> P1Data { get; set; }
        public List<IMeasurement> P2Data { get; set; }
        public List<IMeasurement> PressureData { get; set; }
        public List<IMeasurement> Temperaturedata { get; set; }
        #endregion

        #region Data Extractor Methods
        public string RetrieveSensorDataValues()
        {
            var x = Severity.Error;
            Logger.Log(new Log(MethodBase.GetCurrentMethod().Name, Strings.Sensor.SensorApi.Value + Strings.Config.CountryCode.Value, Severity.Info));
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
                IMeasurement measurement = null;
                ILocation location;

                string country = Strings.String.Unknown.Value;
                double longitude = 0;
                double latitude = 0;
                string measurementName = Strings.String.Unknown.Value;
                double measurementValue = 0;

                JArray jResultsArray = JArray.Parse(json);
                foreach (JObject obj in jResultsArray)
                {
                    // Extract location.
                    JToken jLocation = (JObject)obj.GetValue(Strings.String.Location.Value);
                    foreach (JProperty locationProperty in jLocation)
                    {
                        if (locationProperty.Name.Equals(Strings.String.Country.Value)) country = locationProperty.Value.ToString();
                        if (locationProperty.Name.Equals(Strings.String.Longitude.Value)) longitude = double.Parse(locationProperty.Value.ToString());
                        if (locationProperty.Name.Equals(Strings.String.Latitude)) latitude = double.Parse(locationProperty.Value.ToString());
                    }

                    // Extract sensor data values
                    JArray sensorDataValues = (JArray)obj.GetValue(Strings.Sensor.SensorDataValues.Value);
                    foreach (JObject sensorValue in sensorDataValues)
                    {
                        if (sensorValue.ContainsKey(Strings.Sensor.ValueType.Value))
                        {
                            measurementName = sensorValue.GetValue(Strings.Sensor.ValueType.Value).ToString();
                            measurementValue = double.Parse(sensorValue.GetValue(Strings.String.ValueR.Value).ToString());
                        }
                    }

                    // Dynamically instantiate concrete object of IMeasurement.
                    location = new Location(longitude, latitude, country);
                    measurement = Instantiate(measurementName, measurementValue, location);

                    measurements.Add(measurement);
                }
            }
            catch (Exception e)
            {

            }
            return measurements;
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
            List<Type> types = GetIMeasurementTypes();
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

        /// <summary>
        /// Instantiates an object that matches a constructor that takes as arguments (double ,ILocation).
        /// The method searches for classes that match these costructors only inside Models.
        /// </summary>
        /// <param name="type">The type of the objct to create.</param>
        /// <param name="measurementValue">The measurement value.</param>
        /// <param name="location">The location object</param>
        /// <returns>An instance of the object or null.</returns>
        private dynamic Instantiate(string type, double measurementValue, ILocation location)
        {
            List<Type> types = GetIMeasurementTypes();
            foreach (Type assemblyType in types)
            {
                if (assemblyType.Name.ToLower().Equals(type.ToLower()))
                {
                    return Activator.CreateInstance(assemblyType, measurementValue, location);
                }
            }
            return null;
        }

        /// <summary>
        /// Finds all the types of Models.
        /// </summary>
        /// <returns>All the Model types.</returns>
        private static List<Type> GetIMeasurementTypes()
        {
            return Assembly.UnsafeLoadFrom("Models.dll").GetTypes().ToList();
        }
        #endregion
    }
}
