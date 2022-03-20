using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Models
{
    /// <summary>
    /// Represents the pressure environmental measurement.
    /// </summary>
    public class Pressure : IMeasurement
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Pressure() { }

        /// <summary>
        /// Instantiates a Pressure object with an associated measurement and location.
        /// </summary>
        /// <param name="measurement">The measurement value.</param>
        /// <param name="location">The location of the measurement.</param>
        public Pressure(double measurement, ILocation location)
        {
            this.Measurement = measurement;
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
        public string Name { get { return typeof(Pressure).Name; } }

        /// <summary>
        /// The value of the measurement.
        /// </summary>
        public double Measurement { get; set; }

        /// <summary>
        /// The location of the measurement.
        /// </summary>
        public ILocation Location { get; set; }
        #endregion
    }
}
