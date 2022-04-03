using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementFactories
{
    /// <summary>
    /// Factory for concrete measurement objects.
    /// </summary>
    public class MeasurementsFactory
    {
        /// <summary>
        /// Finds the correct IMeasurement class to instantiate.
        /// </summary>
        /// <returns>A concrete object that implements IMeasurement.</returns>
        public static IMeasurement GetInstance(string name, double value, ILocation location)
        {
            if (name == new Humidity().Name) return new Humidity(value, location);
            if (name == new P1().Name) return new P1(value, location);
            if (name == new P2().Name) return new P2(value, location);
            if (name == new Pressure().Name) return new Pressure(value, location);
            if (name == new Temperature().Name) return new Temperature(value, location);
            return null;
        }
    }
}
