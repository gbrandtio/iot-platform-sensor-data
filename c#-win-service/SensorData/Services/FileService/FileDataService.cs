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
        /// Created a new log file in the configured directory.
        /// </summary>
        /// <param name="logMethod">The log method: could be either for app logs or for data storage.</param>
        /// <returns>The path of the new log file.</returns>
        private static string CreateFile(string logPath)
        {
            try
            {
                logPath = logPath + "log" + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + FileExtensions.Csv.InternalValue;
                isAppendMode = true;
                if (!File.Exists(logPath))
                {
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
                using (var writer = new StreamWriter(path))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<Log>();
                    csv.NextRecord();
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
