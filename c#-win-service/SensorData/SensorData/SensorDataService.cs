using Models;
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

namespace SensorData
{
    public partial class SensorDataService : ServiceBase
    {
        public SensorDataService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Read service configuration and start collecting data.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            SharedValues.GL_GEO_API_KEY = ConfigurationManager.AppSettings[SharedValues.GEO_API_KEY];
        }

        protected override void OnStop()
        {
        }
    }
}
