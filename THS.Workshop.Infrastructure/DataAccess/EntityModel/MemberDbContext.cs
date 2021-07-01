using System.Data.Entity;

namespace THS.Workshop.Infrastructure.DataAccess.EntityModel
{
    public partial class MemberDbContext : DbContext
    {
        public MemberDbContext()
            : base("name=MemberDbContext")
        {
        }

        public virtual DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}