using Digipolis.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mowei.Entities.Models
{
    public class ApplicationRole : IdentityRole<Guid>, IEntityBase
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName.Trim()) { }
        public string Description { set; get; }
        public DateTime CreatedDate { set; get; } = DateTime.Now;

        #region IEntityBase
        [Column(TypeName = "nvarchar(50)")]
        public string Creator { get; set; } = "None";

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(50)")]
        public string LastModifiedBy { get; set; } = "None";

        public DateTime LastModifyDate { get; set; } = DateTime.Now;
        #endregion
    }
}
