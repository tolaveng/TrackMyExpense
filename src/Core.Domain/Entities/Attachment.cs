﻿using Core.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Attachment : BaseEntity<Guid>
    {
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Title { get; set; }

        public string Url { get; set; }
    }
}
