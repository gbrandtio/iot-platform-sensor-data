using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.IO;
using CsvHelper;
using System.Globalization;
using Constants;

namespace Services.FileService
{
    /// <summary>
    /// Responsible for storing the sensor data / measurements to file formats.
    /// </summary>
    public class FileDataService
    {
        private string currentFilePath = String.Empty;
        private static bool isAppendMode = false;
        private static bool isLogAppendMode = false;
        public void Store(Dictionary<Type, List<IMeasurement>> measurements)
        {
            currentFilePath = CreateFile(Strings.Config.DataFilePath.Value);
            Write(measurements, currentFilePath);
        }

        /// <summary>
        /// Created a new log file in the configured directory.
        /// </summary>
        /// <param name="logMethod">The log method: could be either for app logs or for data storage.</param>
        /// <returns>The path of the new log file.</returns>
        private static string CreateFile(string logPath)
        {
            try
            {
                logPath = logPath + "log" + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + FileExtensions.Csv.InternalValue;
                isLogAppendMode = true;
                isAppendMode = true;
                if (!File.Exists(logPath))
                {
                    isLogAppendMode = false;
                    isAppendMode = false;
                    File.Create(logPath).Close();
                }
            }
            catch (Exception e)
            {
            }
            return logPath;
        }

        /// <summary>
        /// Appends all the passed data to the passed path.
        /// </summary>
        /// <param name="data">The data to write.</param>
        /// <param name="path">The path of the log file.</param>
        /// <returns></returns>
        public static bool Write(Log data)
        {
            string path = CreateFile(Strings.Config.LogPath.Value);
            bool isDataLogged = false;
            try
            {
                using (var writer = new StreamWriter(path, isAppendMode))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    if (!isAppendMode)
                    {
                        csv.WriteHeader<Log>();
                        csv.NextRecord();
                    }
                    csv.WriteRecord(data);
                    csv.NextRecord();
                }
                isDataLogged = true;
            }
            catch (Exception e)
            {
            }
            return isDataLogged;
        }

        /// <summary>
        /// Stores the measurement data into the specified file.
        /// </summary>
        /// <param name="measurements"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Write(Dictionary<Type, List<IMeasurement>> measurements, string path)
        {
            bool isDataLogged = false;
            try
            {
                foreach (KeyValuePair<Type, List<IMeasurement>> measurement in measurements)
                {
                    using (var writer = new StreamWriter(path))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        if (!isAppendMode)
                        {
                            csv.WriteHeader<IMeasurement>();
                            csv.NextRecord();
                        }
                        csv.WriteRecords(measurement.Value);
                        csv.NextRecord();
                    }
                }
                isDataLogged = true;
            }
            catch (Exception e)
            {
            }
            return isDataLogged;
        }
    }
}
