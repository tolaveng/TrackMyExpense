using Core.Domain.Enitities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        
        public Guid IconId { get; set; }
        public virtual Icon Icon { get; set; }
        public bool Archived { get; set; }
    }
}
