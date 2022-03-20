using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    /// <summary>
    /// Defines the contract of the location.
    /// </summary>
    public interface ILocation
    {
        #region Properties
        /// <summary>
        /// Longitude.
        /// </summary>
        double Longitude { get; set; }

        /// <summary>
        /// Latitude.
        /// </summary>
        double Latitude { get; set; }

        /// <summary>
        /// Country.
        /// </summary>
        string Country { get; set; }

        /// <summary>
        /// City.
        /// </summary>
        string City { get; set; }
        #endregion
    }
}
