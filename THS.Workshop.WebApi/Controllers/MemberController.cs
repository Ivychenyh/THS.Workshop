using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using THS.Workshop.Infrastructure.DomainModel.Member;
using THS.Workshop.Infrastructure.Logic;
using THS.Workshop.WebApi.ServiceModels;

namespace THS.Workshop.WebApi.Controllers
{
    public class MemberController : ApiController
    {

        public  async Task<IHttpActionResult> Get(string request)
        {
            var filterRequest = JsonConvert.DeserializeObject<MemberFilterRequest>(request);
            var service       = new MemberWorkflow();
            var result        = await service.GetAsync(filterRequest.Filter, filterRequest.GridState, CancellationToken.None);
            return this.Ok(result);
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