using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using THS.Workshop.Infrastructure.DomainModel.Member;
using THS.Workshop.Infrastructure.Logic;

namespace THS.Workshop.WebApi.Controllers
{
    public class MemberController : ApiController
    {
        // GET: api/Member
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Member/5
        public string Get(int id)
        {
            return "value";
        }

        //[HttpPost]
        // POST: api/Member
        public async Task<IHttpActionResult> Post(InsertRequest request)
        {
            var memberWorkflow = new MemberWorkflow();
            var result = await memberWorkflow.Insert(request, CancellationToken.None);

            return this.Ok(result);
        }

        public async Task<IHttpActionResult> Put(UpdateRequest request)
        {
            var service = new MemberWorkflow();
            var result = await service.UpdateAsync(request, CancellationToken.None);
            return this.Ok(result);
        }

        public async Task<IHttpActionResult> Delete(DeleteRequest request)
        {
            var service = new MemberWorkflow();
            var result  = await service.DeleteAsync(request, CancellationToken.None);
            return this.Ok(result);
        } 

    }
}