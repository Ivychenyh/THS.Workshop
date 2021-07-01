using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using THS.Infrastructure;
using THS.Workshop.Infrastructure.DataAccess.EntityModel;
using THS.Workshop.Infrastructure.DomainModel;
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

        public async Task<int> UpdateAsync(UpdateRequest request, CancellationToken cancel)
        {
            var result = 0;
            var toDB = MemberMapper.Map<Member>(request);
            using (var db = MemberDbContext.Create())
            {
                db.Members.Attach(toDB);
                db.Entry(toDB).State = EntityState.Modified;
                result               = await db.SaveChangesAsync(cancel);

            }

            return result;
        }

        public async Task<int> DeleteAsync(DeleteRequest request, CancellationToken cancel)
        {
            var toDb = MemberMapper.Map<Member>(request);
            var result    = 0;
            using (var db = MemberDbContext.Create())
            {
                db.Members.Attach(toDb);
                db.Entry(toDb).State = EntityState.Deleted;
                result = await db.SaveChangesAsync(cancel);
            }

            return result;
        }

        public async Task<List<Member>> QueryAsync(QueryRequest request, GridState gridState, CancellationToken cancel)
        {
            //int pageIndex = 0;
            //int pageSize  = 10;

            //if (gridState != null)
            //{
            //    pageIndex = gridState.PageIndex;
            //    pageSize  = gridState.PageSize;
            //}

            using (var db = MemberDbContext.Create())
            {
                var queryable = db.Members.OrderBy(p => p.Id)
                                  .Skip(gridState.PageIndex).Take(gridState.PageSize)
                                                              .AsNoTracking().Select(p => p);
                if (request.Age != null)
                {
                    queryable = queryable.Where(p => p.Age == request.Age);
                }

                if (!string.IsNullOrWhiteSpace(request.Email))
                {
                    queryable = queryable.Where(p=>p.Email == request.Email);
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    queryable = queryable.Where(p=>p.Name == request.Name);
                }

                if (!string.IsNullOrWhiteSpace(request.Remark))
                {
                    queryable = queryable.Where(p=>p.Remark.Contains(request.Remark));
                }

                var memberFromDB = queryable.AsNoTracking().ToListAsync(cancel);

                return await memberFromDB;
            }
        }
    }
}