using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using TechTalk.SpecFlow;
using THS.Workshop.Infrastructure.DataAccess.EntityModel;
using Microsoft.Owin.Hosting;
using Owin;
using THS.Workshop.WebApi;

namespace THS.Workshop.InfrastructureTest
{
    
    [Binding]
    public class SpecflowHook
    {
        public static readonly string      TestData     = "出發吧，跟我一起進入偉大的航道";
        public static readonly DateTime    TestDateTime = new DateTime(1900, 1, 1, 0, 0, 0);
        public static readonly string      TestUserId   = "TEST_USER";
        private const          string      HOST_ADDRESS = "https://localhost:44316/";
        private static         IDisposable s_webApp;
        public static          HttpClient  s_client;

        
        [AfterScenario]
        public void AfterScenario()
        {
            this.DeleteAll();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            s_webApp.Dispose();
            using (var dbContext = MemberDbContext.Create())
            {
                if (dbContext.Database.Exists())
                {
                    dbContext.Database.Delete();
                }
            }
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            this.DeleteAll();
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var instance = SqlProviderServices.Instance;
            //Mapper.Initialize(p => p.AddProfile(new DefaultMappingProfile()));
            using (var dbContext = MemberDbContext.Create())
            {
                if (dbContext.Database.Exists())
                {
                    dbContext.Database.Delete();
                }

                dbContext.Database.Initialize(true);
            }
            s_webApp = WebApp.Start<Startup>(HOST_ADDRESS);
            Console.WriteLine("Web API started!");
            s_client             = new HttpClient();
            s_client.BaseAddress = new Uri(HOST_ADDRESS);
            Console.WriteLine("HttpClient started!");
           
        }

        public class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                var configuration = new HttpConfiguration();
                WebApiConfig.Register(configuration);
#if DEBUG
                app.UseErrorPage();
#endif
                app.UseWelcomePage("/Welcome");
                app.UseWebApi(configuration);

                //        }
            }
        } 

        public string DeleteAll()
        {
            var sql = @"
-- disable referential integrity
EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL' 


EXEC sp_MSForEachTable 'DELETE FROM ?' 


-- enable referential integrity again 
EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL' 
";
            using (var dbContext = MemberDbContext.Create())
            {
                dbContext.Database.ExecuteSqlCommand(sql);
            }

            return sql;
        }

        public static string GetDeleteCommand(string          testData,
                                              string          columnName = "Remark",
                                              params string[] tableNames)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(testData),   "!string.IsNullOrWhiteSpace(testData)");
            Contract.Requires(!string.IsNullOrWhiteSpace(columnName), "!string.IsNullOrWhiteSpace(columnName)");
            Contract.Requires(tableNames != null,                     "tableNames!=null");

            var commandBuilder = new StringBuilder();
            foreach (var tableName in tableNames)
            {
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    continue;
                }

                var deleteCommand = $@"
delete from [{tableName}]
where {columnName} = N'{testData}'
";

                commandBuilder.AppendLine(deleteCommand);
            }

            return commandBuilder.ToString();
        }

        public static Guid Parse(string id)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(id), "!string.IsNullOrWhiteSpace(id)");
            var _guidFormat = "{0}-0000-0000-0000-000000000000";
            var guidText    = string.Format(_guidFormat, id.PadRight(8, '0'));
            var key         = Guid.Parse(guidText);
            return key;
        }

        private void DeleteBy(string columnName = "Remark")
        {
            IEnumerable<string> tableNames = new List<string>
            {
                "Member"
            };

            var deleteCommand = GetDeleteCommand(TestData, columnName, tableNames.ToArray());
            using (var dbContext = MemberDbContext.Create())
            {
                dbContext.Database.ExecuteSqlCommand(deleteCommand);
            }
        }
    }
}