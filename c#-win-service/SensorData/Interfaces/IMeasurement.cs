using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    /// <summary>
    /// Defines a contract that sensor measurements need to implement.
    /// </summary>
    public interface IMeasurement
    {
        /// <summary>
        /// Name of the measurement.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Value of the measurement.
        /// </summary>
        double Measurement { get; set; }

        /// <summary>
        /// Location of the measurement.
        /// </summary>
        ILocation Location { get; set; }
    }
}
