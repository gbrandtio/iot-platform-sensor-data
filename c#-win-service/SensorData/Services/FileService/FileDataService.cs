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
using System.Diagnostics;

namespace Services.FileService
{
    /// <summary>
    /// Responsible for storing the sensor data / measurements to file formats.
    /// </summary>
    public class FileDataService
    {
        #region Members
        private string currentFilePath = string.Empty;
        private static bool isAppendMode = false;
        private static bool isLogAppendMode = false;
        #endregion

        #region Properties
        /// <summary>
        /// Returns the path of the SensorData.exe.config file.
        /// </summary>
        public static string AppConfigFilePath
        {
            get
            {
                return GetAppConfigurationFilename();
            }
        }
        #endregion

        /// <summary>
        /// Stores all the data passed through the list into the appropriate file.
        /// </summary>
        /// <param name="measurements">A list of measurements to be stored.</param>
        public void Store(Dictionary<Type, List<IMeasurement>> measurements)
        {
            currentFilePath = CreateFile(Strings.Config.DataFilePath.Value);
            Write(measurements, currentFilePath);
        }

        /// <summary>
        /// Created a new log file in the configured directory.
        /// </summary>
        /// <param name="logPath">The log path to create the files.</param>
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
                    if (!isLogAppendMode)
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

        private static string GetAppConfigurationFilename()
        {
            // Extract all confguration keys from app.config.
            string currentProccessName = Process.GetCurrentProcess().ProcessName;
            string appConfigFilePath = Directory.GetCurrentDirectory() + "\\" + currentProccessName + FileExtensions.ExeConfig;

            return appConfigFilePath;
        }
    }
}
