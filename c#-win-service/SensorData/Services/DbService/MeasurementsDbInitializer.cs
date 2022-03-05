using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DbService
{
    public class MeasurementsDbInitializer : DropCreateDatabaseAlways<MeasurementsDbContext>
    {
        protected override void Seed(MeasurementsDbContext context)
        {
            try
            {
                base.Seed(context);
            }
            catch (Exception e)
            {

            }
        }
    }
}
