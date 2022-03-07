using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IParser
    {
        List<string> ExtractData(params object[] args);

        bool ValidateData(string data);
    }
}
