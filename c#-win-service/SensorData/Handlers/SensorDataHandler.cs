﻿using Helpers;
using Interfaces;
using Models;
using Newtonsoft.Json.Linq;
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
        /// <summary>
        /// Iterates through the json response of the sensor API and tries to extract the required measurements
        /// and the location of each sensor.
        /// </summary>
        /// <param name="json">The json response from the </param>
        /// <returns>A List of IMeasurement objects. It is possible for a member of the List to be NULL.</returns>
        public List<IMeasurement> ExtractSensorDataValues(string json)
        {
            List<IMeasurement> measurements = new List<IMeasurement>();
            try
            {
                IMeasurement measurement = null;
                ILocation location;

                string country = SharedValues.UNKNOWN;
                double longitude = 0;
                double latitude = 0;
                string measurementName = SharedValues.UNKNOWN;
                double measurementValue = 0;

                JArray jResultsArray = JArray.Parse(json);
                foreach (JObject obj in jResultsArray)
                {
                    // Extract location.
                    JToken jLocation = (JObject)obj.GetValue("location");
                    foreach (JProperty locationProperty in jLocation)
                    {
                        if (locationProperty.Name.Equals("country")) country = locationProperty.Value.ToString();
                        if (locationProperty.Name.Equals("longitude")) longitude = double.Parse(locationProperty.Value.ToString());
                        if (locationProperty.Name.Equals("latitude")) latitude = double.Parse(locationProperty.Value.ToString());
                    }

                    // Extract sensor data values
                    JArray sensorDataValues = (JArray)obj.GetValue("sensordatavalues");
                    foreach (JObject sensorValue in sensorDataValues)
                    {
                        if (sensorValue.ContainsKey("value_type"))
                        {
                            measurementName = sensorValue.GetValue("value_type").ToString();
                            measurementValue = double.Parse(sensorValue.GetValue("value").ToString());
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

        /// <summary>
        /// Given a type, returns all the objects inside a list that are a concrete implementation of this type.
        /// </summary>
        /// <param name="measurements">List of measurements to get objects from.</param>
        /// <param name="type">Type of objects to add to the return list.</param>
        /// <returns>List of objects with the specified type</returns>
        public List<IMeasurement> GetSpecificMeasurementObjectList(List<IMeasurement> measurements, Type type)
        {
            List<IMeasurement> specificMeasurementObjectList = null;
            foreach (IMeasurement measurement in measurements)
            {
                if (type == measurement.GetType()) specificMeasurementObjectList.Add(measurement);
            }
            return specificMeasurementObjectList;
        }
        #endregion

        #region Private Methods
        private dynamic Instantiate(string type, double measurementValue, ILocation location)
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type assemblyType in types)
            {
                if (assemblyType.Name.ToLower().Equals(type.ToLower()))
                {
                    return Activator.CreateInstance(assemblyType, measurementValue, location);
                }
            }
            return null;
        }
        #endregion
    }
}
