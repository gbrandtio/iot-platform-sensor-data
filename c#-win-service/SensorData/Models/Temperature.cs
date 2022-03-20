using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Models
{
    /// <summary>
    /// Class that represents the temperature environmental measurement.
    /// </summary>
    public class Temperature : IMeasurement
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Temperature() { }

        /// <summary>
        /// Simple constructor to create a Temperature object.
        /// The temperature in Kelvin and Fahreneit is delivered automatically by the respective properties. The temperature measurement
        /// on this constructor must be passed in Celsius degrees.
        /// </summary>
        /// <param name="celsiusMeasurement">The measurement of the temperature in Cesius.</param>
        public Temperature(double celsiusMeasurement, ILocation location)
        {
            this.Measurement = celsiusMeasurement;
            this.Location = location;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Unique identifier - used for the local database.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the measurement.
        /// </summary>
        public string Name { get { return typeof(Temperature).Name; } }

        /// <summary>
        /// The value of the measurement. Must be supplied in Celsius degrees.
        /// </summary>
        public double Measurement { get; set; }

        /// <summary>
        /// The location of the measurement.
        /// </summary>
        public ILocation Location { get; set; }

        /// <summary>
        /// Converts the temperature from Celsius degrees to Kelvin.
        /// </summary>
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

        /// <summary>
        /// Converts the temperature from Celsius degrees to Fahreneit.
        /// </summary>
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
