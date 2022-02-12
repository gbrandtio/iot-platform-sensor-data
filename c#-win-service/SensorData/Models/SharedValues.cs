using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class SharedValues
    {
        // Constants
        public const string GEO_API_KEY = "GEO_API_KEY";
        public const string UNKNOWN = "unknown";

        // Variables that need to be injected globally
        public static string GL_GEO_API_KEY { get; set; }
    }
}
