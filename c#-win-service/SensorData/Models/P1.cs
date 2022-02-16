using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Models
{
    /// <summary>
    /// Class that represents the PM10 environmental measurement.
    /// </summary>
    public class P1 : IMeasurement
    {
        #region Constructor
        public P1() { }
        public P1(double measurement, ILocation location)
        {
            this.Measurement = measurement;
            this.Location = location;
        }
        #endregion

        #region Properties
        public int ID { get; set; }
        public string Name { get { return typeof(Humidity).Name; } }
        public double Measurement { get; set; }
        public ILocation Location { get; set; }
        #endregion
    }
}
