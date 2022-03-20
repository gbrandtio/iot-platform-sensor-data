using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    /// <summary>
    /// Defines a contract that controllers need to implement.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Controls the flow of a specific component.
        /// </summary>
        void Control();
    }
}
