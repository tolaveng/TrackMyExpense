using Core.Domain.Enitities;
using System;

namespace Core.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int CategoryId {  get; set; }

        public string CategoryName { get; set; }

        public string IconName { get; set; }
    }
}
