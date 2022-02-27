using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace ModelTests
{
    [TestClass]
    public class TestModelNames
    {
        /// <summary>
        /// Tests whether the returned name of the Humidity model is the one that is expected.
        /// </summary>
        [TestMethod]
        public void TestHumidityName()
        {
            string expectedHumidityName = "Humidity";
            Humidity humidity = new Humidity();
            string actualHumidityName = humidity.Name;

            Assert.AreEqual(expectedHumidityName, actualHumidityName, "Humidity name is not correct");
        }

        /// <summary>
        /// Tests whether the returned name of the P1 model is the one that is expected.
        /// </summary>
        [TestMethod]
        public void TestP1Name()
        {
            string expectedP1Name = "P1";
            P1 p1 = new P1();
            string actualP1Name = p1.Name;

            Assert.AreEqual(expectedP1Name, actualP1Name, "P1 name is not correct");
        }

        /// <summary>
        /// Tests whether the returned name of the P2 model is the one that is expected.
        /// </summary>
        [TestMethod]
        public void TestP2Name()
        {
            string expectedP2Name = "P2";
            P2 p2 = new P2();
            string actualP2Name = p2.Name;

            Assert.AreEqual(expectedP2Name, actualP2Name, "P2 name is not correct");
        }

        /// <summary>
        /// Tests whether the returned name of the Pressure model is the one that is expected.
        /// </summary>
        [TestMethod]
        public void TestPressureName()
        {
            string expectedPressureName = "Pressure";
            Pressure pressure = new Pressure();
            string actualPressureName = pressure.Name;

            Assert.AreEqual(expectedPressureName, actualPressureName, "Pressure name is not correct");
        }

        /// <summary>
        /// Tests whether the returned name of the Temperature model is the one that is expected.
        /// </summary>
        [TestMethod]
        public void TestemperatureName()
        {
            string expectedTemperatureName = "Temperature";
            Temperature temperature = new Temperature();
            string actualTemperatureName = temperature.Name;

            Assert.AreEqual(expectedTemperatureName, actualTemperatureName, "Temperature name is not correct");
        }
    }
}
