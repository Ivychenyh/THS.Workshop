using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using THS.Workshop.Infrastructure.DataAccess.EntityModel;
using THS.Workshop.Infrastructure.DataAccess.Repository;
using THS.Workshop.Infrastructure.DomainModel;
using THS.Workshop.Infrastructure.DomainModel.Member;

namespace THS.Workshop.Infrastructure.Logic
{
    public class MemberWorkflow: IMemberWorkflow
    {
        
        public async Task<int> Insert(InsertRequest request, CancellationToken cancel)
        {
            var repository = new MemberRepository();
            return await repository.InsertAsync(request, cancel);
        }

        public async Task<int> UpdateAsync(UpdateRequest request, CancellationToken cancel)
        {
            var repository = new MemberRepository();
            return await repository.UpdateAsync(request, cancel);

        }

        public async Task<int> DeleteAsync(DeleteRequest request, CancellationToken cancel)
        {
            var repository = new MemberRepository();
            return await repository.DeleteAsync(request, cancel);
        }

        public async Task<List<Member>> GetAsync(QueryRequest request,GridState gridState, CancellationToken cancel)
        {
            var repository = new MemberRepository();
            return await repository.QueryAsync(request, gridState, cancel);
        }
    }

    public interface IMemberWorkflow
    {
        Task<int> Insert(InsertRequest request, CancellationToken cancel);

        Task<int> UpdateAsync(UpdateRequest request, CancellationToken cancel);

        Task<int> DeleteAsync(DeleteRequest request, CancellationToken cancel);

        Task<List<Member>> GetAsync(QueryRequest request, GridState gridState, CancellationToken cancel);
    }
}
