using System.Threading;
using System.Threading.Tasks;
using THS.Infrastructure;
using THS.Workshop.Infrastructure.DataAccess.EntityModel;
using THS.Workshop.Infrastructure.DomainModel.Member;

namespace THS.Workshop.Infrastructure.DataAccess.Repository
{
    public class MemberRepository
    {
        public async Task<int> InsertAsync(InsertRequest requset, CancellationToken cancel)
        {
            var result = 0;
            var toDB = MemberMapper.Map<Member>(requset);
            //give guid
            toDB.Id = SequentialGuid.NewGuid();
            using (var db = new MemberDbContext())
            {
                db.Members.Add(toDB);
                result = await db.SaveChangesAsync(cancel);
            }
            return result;
        }
    }
}