using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Class that represents the PM2.5 environmental measurement.
    /// </summary>
    class P2
    {
        #region Constructor
        public P2() { }
        public P2(double measurement, Location location)
        {
            this.Measurement = measurement;
            this.Location = location;
        }
        #endregion

        #region Properties
        public double Measurement { get; set; }
        public Location Location { get; set; }
        #endregion
    }
}
