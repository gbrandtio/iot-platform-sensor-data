using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Models
{
    /// <summary>
    /// Represents the concrete class of the Humidity measurement.
    /// </summary>
    public class Humidity : IMeasurement
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Humidity() { }

        /// <summary>
        /// Constructor that instantiates an object with the measurement value and location.
        /// </summary>
        /// <param name="measurement">The value of the measurement.</param>
        /// <param name="location">The location of the measurement.</param>
        public Humidity(double measurement, ILocation location)
        {
            this.Measurement = measurement;
            this.Location = location;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Unique identifier, used for the local database.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the measurement.
        /// </summary>
        public string Name { get { return typeof(Humidity).Name; } }

        /// <summary>
        /// The value of the measurement.
        /// </summary>
        public double Measurement { get; set; }

        /// <summary>
        /// Represantation of the Location of the measurement.
        /// </summary>
        public ILocation Location { get; set; }
        #endregion
    }
}
