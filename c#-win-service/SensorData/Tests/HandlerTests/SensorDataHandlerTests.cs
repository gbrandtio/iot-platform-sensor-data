using System;
using System.Collections.Generic;
using Handlers;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HandlerTests
{
    [TestClass]
    public class SensorDataHandlerTests
    {
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
                if (measurement != null && !String.IsNullOrEmpty(measurement.Name)
                    && measurement.Measurement != 0.0) containsAtLeastOneMeasurement = true;
            }
            Assert.IsTrue(containsAtLeastOneMeasurement, "All objects in the extracted data are null");
        }

        [TestMethod]
        public void TestSeparatedMeasurementLists()
        {

        }
    }
}
