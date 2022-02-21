using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Config_Models;
using Interfaces;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace Services.FileClient
{
    /// <summary>
    /// Responsible for storing the sensor data / measurements to file formats.
    /// </summary>
    public class FileDataService
    {
        private string currentFilePath = String.Empty;
        public void Store(Dictionary<Type, List<IMeasurement>> measurements)
        {
            if (FileLimitExceeded(currentFilePath) || String.IsNullOrEmpty(currentFilePath)) currentFilePath = CreateFile(Strings.Config.DataFilePath.Value);
            Write(measurements, currentFilePath);
        }

        /// <summary>
        /// Checks if the file size of a file has exceeded the configured max file size.
        /// </summary>
        /// <param name="path">The file to check.</param>
        /// <returns>True if the file size has been exceeded.</returns>
        public static bool FileLimitExceeded(string path)
        {
            try
            {
                if (!File.Exists(path)) return false;

                FileInfo fileInfo = new FileInfo(path);
                long bytes = fileInfo.Length;
                long kiloBytes = bytes / 1024;
                if (kiloBytes > Strings.Config.MaxFileSize.ValueLong)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        /// <summary>
        /// A new log file is created every 24:00.
        /// This is to avoid huge log files as well as to be able to get specific logs
        /// in case of system failure.
        /// </summary>
        /// <returns>True is midnight and we need to create a new log file.</returns>
        public static bool CheckFileCutoff()
        {
            bool isTimeToCutoff = false;
            if (DateTime.Now.TimeOfDay > new TimeSpan(24, 0, 0))
            {
                isTimeToCutoff = true;
            }
            return isTimeToCutoff;
        }

        /// <summary>
        /// Created a new log file in the configured directory.
        /// </summary>
        /// <param name="logMethod">The log method: could be either for app logs or for data storage.</param>
        /// <returns>The path of the new log file.</returns>
        public static string CreateFile(string logMethod)
        {
            string logPath = Strings.Config.DataFilePath.Value;
            if (logMethod.Equals(Strings.Config.LogPath.Value)) logPath = Strings.Config.LogPath.Value;

            try
            {
                logPath = logPath + "-" + DateTime.Now.Date.ToString() + FileExtensions.Csv;
                File.Create(logPath);
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
        public static bool Write(Log data, string path)
        {
            bool isDataLogged = false;
            try
            {
                using (var writer = new StreamWriter(path))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
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
        /// 
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
