using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using SchoolSite.Web.Areas.Admin.Models;

namespace SchoolSite.Web.DAL.MySql
{
    public class MySqlInitializer : IDatabaseInitializer<ApplicationDbContext>
    {
        public void InitializeDatabase(ApplicationDbContext context)
        {
            try
            {
                if (!context.Database.Exists())
                {
                    context.Database.Create();
                }
                else
                {
                    var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
                    string.Format(
                      "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{0}' AND table_name = '__MigrationHistory'",
                      "SchoolSite"));

                    if (migrationHistoryTableExists.FirstOrDefault() == 0)
                    {
                        context.Database.CreateIfNotExists();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}