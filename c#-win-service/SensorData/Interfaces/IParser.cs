using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IParser
    {
        #region Methods
        string ExtractData(string json);
        string ExtractSpecificInfo(string formattedAddress, int pos, char delimeter);
        #endregion
    }
}
