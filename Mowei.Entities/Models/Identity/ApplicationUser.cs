using Digipolis.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mowei.Entities.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>, IEntityBase
    {
        #region IdentityUser

        [DisplayName("啟用二階段驗證")]
        public override bool TwoFactorEnabled { get; set; }

        [DisplayName("手機號碼已認證")]
        public override bool PhoneNumberConfirmed { get; set; }

        [DisplayName("手機號碼")]
        public override string PhoneNumber { get; set; }

        [DisplayName("並發戳記")]
        public override string ConcurrencyStamp { get; set; }

        [DisplayName("安全戳記")]
        public override string SecurityStamp { get; set; }

        [DisplayName("密碼Hash")]
        public override string PasswordHash { get; set; }

        [DisplayName("電子信箱已認證")]
        public override bool EmailConfirmed { get; set; }

        [DisplayName("標準化電子信箱")]
        public override string NormalizedEmail { get; set; }

        [DisplayName("電子信箱")]
        public override string Email { get; set; }

        [DisplayName("標準化使用者帳號")]
        public override string NormalizedUserName { get; set; }

        [DisplayName("使用者帳號")]
        public override string UserName { get; set; }

        [DisplayName("啟用鎖定")]
        public override bool LockoutEnabled { get; set; }

        [DisplayName("失敗次數")]
        public override int AccessFailedCount { get; set; }

        [DisplayName("鎖定到期時間")]
        public override DateTimeOffset? LockoutEnd { get; set; }

        #endregion

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
