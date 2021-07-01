using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace THS.Workshop.Infrastructure.DataAccess.EntityModel
{
    public partial class MemberDbContext : DbContext
    {
       
        public static MemberDbContext Create()
        {
            var db = new MemberDbContext();
            db.Configuration.ValidateOnSaveEnabled    = false;
            db.Configuration.LazyLoadingEnabled       = false;
            db.Configuration.ProxyCreationEnabled     = false;
            db.Configuration.AutoDetectChangesEnabled = false;
            return db;
        }
    }
}
