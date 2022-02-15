using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ILocation
    {
        #region Properties
        double Longitude { get; set; }
        double Latitude { get; set; }
        string Country { get; set; }
        string City { get; set; }
        #endregion
    }
}
