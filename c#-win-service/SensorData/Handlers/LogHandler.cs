using Models;
using Models.Config_Models;
using Services.FileClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers
{
    /// <summary>
    /// Responsible for maintaining the application logs.
    /// </summary>
    public class Logger
    {
        private static string currentLogFile = String.Empty;
        public static void Log(Log data)
        {
            if (FileDataService.CheckFileCutoff() || String.IsNullOrEmpty(currentLogFile)) currentLogFile = FileDataService.CreateFile(Strings.Config.LogPath.Value);
            FileDataService.Write(data, currentLogFile);
        }
    }
}
