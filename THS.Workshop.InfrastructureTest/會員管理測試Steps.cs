using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using THS.Workshop.Infrastructure.DataAccess.EntityModel;
using THS.Workshop.Infrastructure.DataAccess.Repository;
using THS.Workshop.Infrastructure.DomainModel.Member;

namespace THS.Workshop.InfrastructureTest
{
    [Binding]
    public class 會員管理測試Steps : Steps
    {
        [Given(@"前端應傳來以下新增請求資料")]
        public void Given前端應傳來以下新增請求資料(Table table)
        {
            var request = table.CreateInstance<InsertRequest>();
            this.ScenarioContext.Set(request, "request");
        }
        
        [When(@"調用新增")]
        public void When調用新增()
        {
            var repository = new MemberRepository();
            var requset    = this.ScenarioContext.Get<InsertRequest>("request");
            var count      = repository.InsertAsync(requset, CancellationToken.None).Result;
        }
        
        [Then(@"預期資料庫的 Member 資料表應有以下資料")]
        public void Then預期資料庫的Member資料表應有以下資料(Table expected)
        {
            //query db
            using (var db = new MemberDbContext())
            {
                var actual = db.Members.AsNoTracking().ToList();
                expected.CompareToSet(actual);
            }
        }

    }
}
