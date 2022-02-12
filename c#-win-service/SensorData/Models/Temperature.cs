using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Temperature
    {
        #region Constructor
        /// <summary>
        /// Simple constructor to create a Temperature object.
        /// The temperature in Kelvin and Fahreneit is delivered automatically by the respective properties.
        /// </summary>
        /// <param name="celsiusMeasurement">The measurement of the temperature in Cesius.</param>
        /// <param name="location">The location of the sensor.</param>
        public Temperature(double celsiusMeasurement, Location location)
        {
            this.Celsius = celsiusMeasurement;
            this.Location = location;
        }
        #endregion

        #region Properties
        public Location Location { get; set; }
        public double Celsius { get; set; }
        public double Kelvin
        {
            get
            {
                return Celsius + 273.15;
            }
            set
            {
                Celsius = value - 273.15;
            }
        }
        public double Fahrenheit
        {
            get
            {
                return Celsius * 9 / 5 + 32;
            }
            set
            {
                Celsius = (value - 32) * 5 / 9;
            }
        }
        #endregion
    }
}
