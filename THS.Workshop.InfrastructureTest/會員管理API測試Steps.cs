using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using THS.Workshop.Infrastructure.DataAccess.EntityModel;
using THS.Workshop.Infrastructure.DomainModel;
using THS.Workshop.Infrastructure.DomainModel.Member;

namespace THS.Workshop.InfrastructureTest
{
    [Binding]
    public class 會員管理API測試Steps : Steps
    {
        [When(@"調用Post新增會員資料")]
        public void When調用Post新增會員資料()
        {
            var request = this.ScenarioContext.Get<InsertRequest>("request");
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = SpecflowHook.s_client.PostAsync("api/Member/Post", content).Result;
            ScenarioContext.Set(response.StatusCode, "statusCode");
        }

        [Then(@"預期HttpStatusCode為(.*)")]
        public void Then預期HttpStatusCode為(HttpStatusCode expected)
        {
            var actual = this.ScenarioContext.Get<HttpStatusCode>("statusCode");
            Assert.AreEqual(expected, actual);
        }

        [Given(@"資料庫的Member資料表已存在以下資料")]
        public void Given資料庫的Member資料表已存在以下資料(Table table)
        {
            //var toDB = table.CreateInstance<Member>();
            var toDB = table.CreateSet<Member>();
            using (var db = MemberDbContext.Create())
            {
                //db.Members.Add(toDB);
                db.Members.AddRange(toDB);
                db.SaveChanges();
            }
        }

        [Given(@"前端傳來以下UpdateRequest")]
        public void Given前端傳來以下UpdateRequest(Table table)
        {
            var request = table.CreateInstance<UpdateRequest>();
            this.ScenarioContext.Set(request, "request");
        }

        [When(@"調用Put '(.*)'")]
        public void When調用Put(string p0)
        {
            var requestContent = this.ScenarioContext.Get<UpdateRequest>("request");
            var json = JsonConvert.SerializeObject(requestContent);
            var sringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = SpecflowHook.s_client.PutAsync("api/Member/Put", sringContent);
            var statusCode = response.Result.StatusCode;
            Console.WriteLine(response.Result);
            this.ScenarioContext.Set(statusCode, "statusCode");
        }

        [Then(@"預期資料庫的Member資料表有以下資料")]
        public void Then預期資料庫的Member資料表有以下資料(Table expected)
        {
            using (var db = MemberDbContext.Create())
            {
                var actual = db.Members.AsNoTracking().ToList();

                expected.CompareToSet(actual);
            }
        }

        [Given(@"前端傳來以下DeleteRequest")]
        public void Given前端傳來以下DeleteRequest(Table table)
        {
            var request = table.CreateInstance<DeleteRequest>();
            this.ScenarioContext.Set(request, "request");
        }

        [When(@"調用Delete '(.*)'")]
        public void When調用Delete(string url)
        {
            var request = this.ScenarioContext.Get<DeleteRequest>("request");
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = SpecflowHook.s_client.SendAsync(new HttpRequestMessage(HttpMethod.Delete, url) { Content = content }).Result;
            this.ScenarioContext.Set(response.StatusCode, "statusCode");
        }

        [When(@"調用查詢Get  '(.*)'")]
        public void When調用查詢Get(string url)
        {
            var request = this.ScenarioContext.Get<QueryRequest>("request");
            var gridState = this.ScenarioContext.Get<GridState>("gridState");
            var data = new
            {
                Filter = request,
                GridState = gridState
            };
            var json = JsonConvert.SerializeObject(data);
            //Console.WriteLine(json);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{url}?request={json}");
            //{
            //    Content = new StringContent(json, Encoding.UTF8, "application/json")
            //};<---System.Net.ProtocolViolationException: Cannot send a content-body with this verb-type.

            var responseOld = SpecflowHook.s_client.SendAsync(requestMessage).Result;
            var response = SpecflowHook.s_client.GetAsync($"{url}?request={json}").Result;
            this.ScenarioContext.Set(response.StatusCode, "statusCode");

            var actual = response.Content.ReadAsAsync<IEnumerable<Member>>().Result;
            this.ScenarioContext.Set(actual, "actual");

            this.ScenarioContext.Set(responseOld.Content.ReadAsAsync<IEnumerable<Member>>().Result, "actualOld");
        }

        [Then(@"預期查詢結果有以下資料use actualOld")]
        public void Then預期查詢結果有以下資料UseActualOld(Table expected)
        {
            var actualOld = this.ScenarioContext.Get<List<Member>>("actualOld");
            expected.CompareToSet(actualOld);
        }


        [When(@"DoNothing")]
        public void WhenDoNothing()
        {
        }
    }
}