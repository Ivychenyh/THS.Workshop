using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using THS.Workshop.Infrastructure.DataAccess.Repository;
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

    }

    public interface IMemberWorkflow
    {
        Task<int> Insert(InsertRequest request, CancellationToken cancel);
    }
}
