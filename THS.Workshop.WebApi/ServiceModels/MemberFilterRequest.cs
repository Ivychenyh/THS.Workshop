using THS.Workshop.Infrastructure.DomainModel;
using THS.Workshop.Infrastructure.DomainModel.Member;

namespace THS.Workshop.WebApi.ServiceModels
{
    public class MemberFilterRequest
    {
        public QueryRequest Filter { get; set; }

        public GridState GridState { get; set; }
    }
}