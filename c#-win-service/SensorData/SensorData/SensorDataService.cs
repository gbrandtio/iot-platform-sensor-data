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
using Factories;

namespace SensorData
{
    public partial class SensorDataService : ServiceBase
    {
        #region members
        private Timer queryTimer; // Timer that triggers the query of the data every time it elapses (config TimerInterval).
        #endregion

        #region Constructor
        public SensorDataService()
        {
            InitializeComponent();
        }
        #endregion

        #region Overriden Service Methods
        /// <summary>
        /// Read service configuration and start collecting data.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            StartTimer();
        }

        protected override void OnContinue()
        {
            StartTimer();
            base.OnContinue();
        }
        protected override void OnStop()
        {
            StopTimer();
            base.Dispose();
            base.OnStop();
        }

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
            queryTimer = new Timer();
            queryTimer.Elapsed += QueryData;
            queryTimer.Interval = int.Parse(ConfigurationManager.AppSettings[Strings.Config.TimerInterval.Value]);
            queryTimer.Enabled = true;
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
            queryTimer.Stop();
            IDataHandler sensorDataHandler = new SensorDataHandlerFactory().GetDataHandler();
            IDataHandler geocodeDataHandler = new GeocodeDataHandlerFactory().GetDataHandler();
            IDataHandler dataStorageHandler = new DataStorageHandlerFactory().GetDataHandler();
            Service.StartDataCollection(sensorDataHandler, geocodeDataHandler, dataStorageHandler);
            queryTimer.Start();
        }
        #endregion
    }
}
