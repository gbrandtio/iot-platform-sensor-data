using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    /// <summary>
    /// Provides all the common file extensions.
    /// </summary>
    public class FileExtensions
    {
        // File Extensions
        private const string R_EXE_CONFIG = ".exe.config";
        private const string R_TXT = ".txt";
        private const string R_CSV = ".csv";
        private FileExtensions(string value)
        {
            this.InternalValue = value;
        }

        /// <summary>
        /// The string value at each moment.
        /// </summary>
        public string InternalValue { get; private set; }
        /// <summary>
        /// .exe
        /// </summary>
        public static FileExtensions ExeConfig { get { return new FileExtensions(R_EXE_CONFIG); } }
        /// <summary>
        /// .txt
        /// </summary>
        public static FileExtensions Txt { get { return new FileExtensions(R_TXT); } }
        /// <summary>
        /// .csv
        /// </summary>
        public static FileExtensions Csv { get { return new FileExtensions(R_CSV); } }
    }
}
