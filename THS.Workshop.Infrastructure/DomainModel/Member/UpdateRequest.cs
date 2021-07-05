using System;
using System.ComponentModel.DataAnnotations;

namespace THS.Workshop.Infrastructure.DomainModel.Member
{
    public class UpdateRequest
    {
        public Guid Id { get; set; }

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