using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using THS.Workshop.Infrastructure.DomainModel.Member;

namespace THS.Workshop.InfrastructureTest
{

    [Binding]
    public class 會員管理API測試Steps : Steps
    {
        [When(@"調用Post新增會員資料")]
        public void When調用Post新增會員資料()
        {
            var request  = this.ScenarioContext.Get<InsertRequest>("request");
            var json     = JsonConvert.SerializeObject(request) ;
            var content  = new StringContent( json, Encoding.UTF8, "application/json");
            var response = SpecflowHook.s_client.PostAsync("api/Member/Post", content).Result;
            ScenarioContext.Set(response.StatusCode, "actual");
        }

        [Then(@"預期HttpStatusCode為(.*)")]
        public void Then預期HttpStatusCode為(HttpStatusCode p0)
        {
            var actual = this.ScenarioContext.Get<HttpStatusCode>("actual");
            Assert.AreEqual(p0, actual);
        }

    }
   
}