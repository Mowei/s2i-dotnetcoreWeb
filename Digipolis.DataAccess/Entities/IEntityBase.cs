using System;

namespace Digipolis.DataAccess.Entities
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
        string Creator { get; set; }
        DateTime CreateDate { get; set; }
        string LastModifiedBy { get; set; }
        DateTime LastModifyDate { get; set; }
    }
}
