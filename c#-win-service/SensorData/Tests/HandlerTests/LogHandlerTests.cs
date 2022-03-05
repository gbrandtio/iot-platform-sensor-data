using CsvHelper;
using Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Constants;

namespace HandlerTests
{
    [TestClass]
    public class LogHandlerTests
    {
        [TestMethod]
        public void TestLogger()
        {
            LogHandler.Log(new Log(MethodBase.GetCurrentMethod().Name, "test", Severity.Info));

            //Check that entry has been logged. The file format of the logs is log-yyyyMMdd.csv.
            string currentLogFile = "log-" + DateTime.Now.ToString("yyyyMMdd") + FileExtensions.Csv.InternalValue;
            List<Log> records = null;

            using (var reader = new StreamReader(Strings.Config.LogPath.Value+ currentLogFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                records = csv.GetRecords<Log>().ToList();
            }

            if (records == null) Assert.Fail("Log could not get written.");

            // Try to see if the test record has been logged.
            bool isDataFound = false;
            int i = 0;
            foreach (Log record in records)
            {
                if (record.Data.Equals("test")) isDataFound = true;
            }
            Assert.IsTrue(isDataFound, "The record could not be retrieved.");
        }
    }
}
