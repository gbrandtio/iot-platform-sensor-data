using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DbClient
{
    public class MeasurementsDbContext : DbContext
    {
        public MeasurementsDbContext() : base("MsrmntsDBConnectionString")
        {
            Database.SetInitializer<MeasurementsDbContext>(new MeasurementsDbInitializer());
        }

        public DbSet<Humidity> Humidity { get; set; }
        public DbSet<P1> P1 { get; set; }
        public DbSet<P2> P2 { get; set; }
        public DbSet<Pressure> Pressure { get; set; }
        public DbSet<Temperature> Temperature { get; set; }
    }
}
