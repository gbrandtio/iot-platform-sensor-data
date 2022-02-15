using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Interfaces;

namespace Models
{
    public class Temperature : IMeasurement
    {
        #region Constructor
        public Temperature() { }
        /// <summary>
        /// Simple constructor to create a Temperature object.
        /// The temperature in Kelvin and Fahreneit is delivered automatically by the respective properties. The temperature measurement
        /// on this constructor must be passed in Celsius degrees.
        /// </summary>
        /// <param name="celsiusMeasurement">The measurement of the temperature in Cesius.</param>
        public Temperature(double celsiusMeasurement, Location location)
        {
            this.Measurement = celsiusMeasurement;
            this.Location = location;
        }
        #endregion

        #region Properties
        public double Measurement { get; set; } // Measurement of temperature supplied in Celsius degrees.
        public ILocation Location { get; set; }
        public double Kelvin
        {
            get
            {
                return Measurement + 273.15;
            }
            set
            {
                Measurement = value - 273.15;
            }
        }
        public double Fahrenheit
        {
            get
            {
                return Measurement * 9 / 5 + 32;
            }
            set
            {
                Measurement = (value - 32) * 5 / 9;
            }
        }
        #endregion
    }
}
