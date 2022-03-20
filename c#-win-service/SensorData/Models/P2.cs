using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Models
{
    /// <summary>
    /// Class that represents the PM2.5 environmental measurement.
    /// </summary>
    public class P2 : IMeasurement
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public P2() { }

        /// <summary>
        /// Instantiates a P2 object with an associated measurement and location.
        /// </summary>
        /// <param name="measurement">The measurement value.</param>
        /// <param name="location">The location of the measurement.</param>
        public P2(double measurement, ILocation location)
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
        public string Name { get { return typeof(P2).Name; } }

        /// <summary>
        /// The measurement's value.
        /// </summary>
        public double Measurement { get; set; }

        /// <summary>
        /// The location of the measurement.
        /// </summary>
        public ILocation Location { get; set; }
        #endregion
    }
}
