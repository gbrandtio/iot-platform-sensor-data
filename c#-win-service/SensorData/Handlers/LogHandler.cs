using Services.FileService;
using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers
{
    /// <summary>
    /// Responsible for maintaining the application logs.
    /// </summary>
    public class LogHandler
    {
        private static string currentLogFile = String.Empty;

        /// <summary>
        /// Logs the passed data into the configured file.
        /// </summary>
        /// <param name="data">The data to be logged.</param>
        public static void Log(Log data)
        {
            FileDataService.Write(data);
        }
    }
}
