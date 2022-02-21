using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Log
    {
        public enum Severity
        {
            Info = 1,
            Warning = 2,
            Error = 3,
            Exception =4
        }

        public Log(string methodName, string data, Severity level)
        {
            this.MethodName = methodName;
            this.Data = data;
            this.Level = level;
        }
        public DateTime Timestamp { get { return DateTime.Now; } }
        public string MethodName { get; set; }
        public string Data { get; set; }
        public Severity Level { get; set; }
    }
}
