using Digipolis.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mowei.Entities.Models
{
    public class Project : EntityBase
    {
        [Required]
        [DisplayName("專案名稱")]
        public string Name { get; set; }
    }
}
