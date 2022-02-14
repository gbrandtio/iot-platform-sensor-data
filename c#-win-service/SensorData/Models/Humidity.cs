using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Humidity
    {
        #region Constructor
        public Humidity() { }
        public Humidity(double measurement, Location location)
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
