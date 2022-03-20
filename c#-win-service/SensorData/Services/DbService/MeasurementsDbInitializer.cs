using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.DbService
{
    /// <summary>
    /// Initializes the entity database. Specifies also the strategy that will be used in case the 
    /// models change and do not match the table models anymore.
    /// </summary>
    public class MeasurementsDbInitializer : DropCreateDatabaseAlways<MeasurementsDbContext>
    {
        /// <summary>
        /// Handles the strategy that will be used when the models change and are not matching the 
        /// existing database tables.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MeasurementsDbContext context)
        {
            try
            {
                base.Seed(context);
            }
            catch (Exception e)
            {
                FileService.FileDataService.Write(new Models.Log(MethodBase.GetCurrentMethod().Name, e.ToString(), Models.Severity.Exception));
            }
        }
    }
}
