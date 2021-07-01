namespace THS.Workshop.Infrastructure.DataAccess.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Member")]
    public partial class Member
    {
        public Guid Id { get; set; }

        public long SequenceId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
