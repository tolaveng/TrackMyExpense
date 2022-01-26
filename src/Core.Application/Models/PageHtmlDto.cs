using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class PageHtmlDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public PageHtmlDto()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Title = string.Empty;
            Content = string.Empty;
        }
    }
}
