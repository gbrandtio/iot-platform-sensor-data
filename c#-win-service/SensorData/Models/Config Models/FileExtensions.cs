using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config_Models
{
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

        public string InternalValue { get; private set; }
        public static FileExtensions ExeConfig { get { return new FileExtensions(R_EXE_CONFIG); } }
        public static FileExtensions Txt { get { return new FileExtensions(R_TXT); } }
        public static FileExtensions Csv { get { return new FileExtensions(R_CSV); } }
    }
}
