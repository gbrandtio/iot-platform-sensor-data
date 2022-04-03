using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Reflection;
using Constants;
using Interfaces;
using Handlers;

namespace SensorData
{
    /// <summary>
    /// Entry point of the application. Provides functionality to periodically
    /// perform application related actions and specifies actions to be taken
    /// upon receiving Service Control Manager (SCM) commands.
    /// </summary>
    public partial class SensorDataService : ServiceBase
    {
        #region members
        private Timer queryTimer; // Timer that triggers the query of the data every time it elapses (config TimerInterval).
        private int queryTimerInterval; // The interval of the timer. Should be initialized with the configured interval.
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the timer based on the configured interval
        /// and calls InitializeComponent function.
        /// </summary>
        public SensorDataService()
        {
            InitializeComponent();
            try
            {
                queryTimer = new Timer();
                queryTimerInterval = int.Parse(Strings.Config.TimerInterval.Value);
                StartTimer();
            }
            catch (Exception e)
            {
                EventLog.WriteEntry(MethodBase.GetCurrentMethod().Name, e.ToString(), EventLogEntryType.Error);
            }
        }
        #endregion

        #region Overriden Service Methods
        /// <summary>
        /// Read service configuration and start collecting data.
        /// Specifies actions to be taken when the service starts.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            StartTimer();
            base.OnStart(args);
        }

        /// <summary>
        /// Specifies actions to take when a service continues normal execution
        /// after being paused.
        /// </summary>
        protected override void OnContinue()
        {
            StartTimer();
            base.OnContinue();
        }

        /// <summary>
        /// Specifies actions to take when the service stops.
        /// </summary>
        protected override void OnStop()
        {
            StopTimer();
            base.Dispose();
            base.OnStop();
        }

        /// <summary>
        /// Specifies actions to take when the service is paused.
        /// </summary>
        protected override void OnPause()
        {
            StopTimer();
            base.OnPause();
        }
        #endregion

        #region Application Methods
        /// <summary>
        /// Starts the query timer and listens for the elapsed event.
        /// When the timer elapses, the application queries the sensor data API.
        /// </summary>
        private void StartTimer()
        {
            queryTimer.Elapsed += QueryData;
            queryTimer.Interval = queryTimerInterval;
            queryTimer.Enabled = true;
            queryTimer.AutoReset = false;
        }

        /// <summary>
        /// Stops the query timer.
        /// </summary>
        private void StopTimer()
        {
            queryTimer.Stop();
            queryTimer.Dispose();
        }

        /// <summary>
        /// Query the data of the sensor API.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Elapsed event arguments</param>
        private void QueryData(object sender, ElapsedEventArgs e)
        {
            #region Configuration Logging
            EventLog.WriteEntry(MethodBase.GetCurrentMethod().Name,
            "Configurations: "
            + "\nTimer Interval: " + Strings.Config.TimerInterval.Value
            + "\nCountry code: " + Strings.Config.CountryCode.Value
            + "\nData file path: " + Strings.Config.DataFilePath.Value
            + "\nData storage mode: " + Strings.Config.DataStorageMode.Value
            + "\nLogs path: " + Strings.Config.LogPath.Value
            + "\nMaximum file size: " + Strings.Config.MaxFileSize.Value
            , EventLogEntryType.Information);
            #endregion
            queryTimer.Stop();
            SensorDataController.ServiceController sensorDataServiceController = new SensorDataController.ServiceController(new SensorDataHandler(), new GeocodeDataHandler(), new DataStorageHandler());
            sensorDataServiceController.Control();
            queryTimer.Start();
        }
        #endregion
    }
}
