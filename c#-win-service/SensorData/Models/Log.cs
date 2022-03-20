using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Represents the severity of the message to be logged.
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// Information
        /// </summary>
        Info = 1,
        /// <summary>
        /// Warning
        /// </summary>
        Warning = 2,
        /// <summary>
        /// Error
        /// </summary>
        Error = 3,
        /// <summary>
        /// Exception
        /// </summary>
        Exception = 4
    }

    /// <summary>
    /// Represents the log data model.
    /// </summary>
    public class Log
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Log() { }

        /// <summary>
        /// Instantiates a Log object with an associated method name, data to be logged, and level of severity.
        /// </summary>
        /// <param name="methodName">The method name that constructs the log data.</param>
        /// <param name="data">The actual data to be logged.</param>
        /// <param name="level">The level of severity of the log message.</param>
        public Log(string methodName, string data, Severity level)
        {
            this.MethodName = methodName;
            this.Data = data;
            this.Level = level;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The timestamp of each log.
        /// </summary>
        public DateTime Timestamp { get { return DateTime.Now; } }

        /// <summary>
        /// The method name that provides the log data.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// The actual data to be logged.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// The severity level of the log message.
        /// </summary>
        public Severity Level { get; set; }
        #endregion
    }
}
