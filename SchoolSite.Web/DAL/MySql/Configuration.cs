using System.Data.Entity.Migrations;
using MySql.Data.Entity;
using SchoolSite.Web.Areas.Admin.Models;

namespace SchoolSite.Web.DAL.MySql
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            // register mysql code generator
            //SetSqlGenerator("MySql.Data.MySqlClient",   new MySqlMigrationSqlGenerator());
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

        protected override void Seed(ApplicationDbContext context)
        {

        }
    }
}