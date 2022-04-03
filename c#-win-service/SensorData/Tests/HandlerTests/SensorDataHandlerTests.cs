using System;
using System.Collections.Generic;
using Handlers;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace HandlerTests
{
    /// <summary>
    /// Unit testing of SensorDataHandler class.
    /// </summary>
    [TestClass]
    public class SensorDataHandlerTests
    {
        /// <summary>
        /// Tests that the configured API is valid and returns a response.
        /// </summary>
        [TestMethod]
        public void TestDataRetrieval()
        {
            SensorDataHandler sensorDataHandler = new SensorDataHandler();
            string sensorApiResponse = sensorDataHandler.RetrieveSensorDataValues();

            if (String.IsNullOrEmpty(sensorApiResponse)) Assert.Fail("No response from configured API.");
        }

        /// <summary>
        /// Test that the sensor data retrieved from the API can be extracted gracefully.
        /// </summary>
        [TestMethod]
        public void TestDataExtraction()
        {
            SensorDataHandler sensorDataHandler = new SensorDataHandler();
            string apiResponse = sensorDataHandler.RetrieveSensorDataValues();
            if (string.IsNullOrEmpty(apiResponse)) Assert.Fail("No response from configured API");

            List<IMeasurement> extractedData = sensorDataHandler.ExtractSensorDataValues(apiResponse);
            bool containsAtLeastOneMeasurement = false;
            foreach (IMeasurement measurement in extractedData)
            {
                if (measurement != null) containsAtLeastOneMeasurement = true;
            }
            Assert.IsTrue(containsAtLeastOneMeasurement, "All objects in the extracted data are null");
        }

        /// <summary>
        /// Test that a list containing various concrete objects of IMeasurement
        /// can be split into a dictionary of specific object lists.
        /// </summary>
        [TestMethod]
        public void TestSeparatedMeasurementsList()
        {
            List<IMeasurement> measurements = new List<IMeasurement>();
            for (int i=0; i<4; i++)
            {
                measurements.Add(new Humidity());
                measurements.Add(new P1());
                measurements.Add(new P2());
                measurements.Add(new Pressure());
                measurements.Add(new Temperature());
            }

            if (measurements.Count == 0) Assert.Fail("Measurements are 0.");

            SensorDataHandler sensorDataHandler = new SensorDataHandler();
            Dictionary<string, List<IMeasurement>> dicSeparatedLists = sensorDataHandler.GetSeparatedMeasurementLists(measurements);
            foreach(KeyValuePair<string, List<IMeasurement>> pair in dicSeparatedLists)
            {
                string measurementType = pair.Key;
                List<IMeasurement> internalMeasurements = pair.Value;

                if (internalMeasurements.Count == 0) Assert.Fail("No measurements found on separated measurements list.");

                foreach (IMeasurement measurement in internalMeasurements)
                {
                    if (measurement.Name != measurementType) Assert.Fail("Not all measurements are of the same type on the separated list.");
                }
            }
        }
    }
}
