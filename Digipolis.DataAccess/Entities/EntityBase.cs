using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digipolis.DataAccess.Entities
{
    public class EntityBase: IEntityBase
    {
        // This is the base class for all entities.
        // The DataAccess repositories have this class as constraint for entities that are persisted in the database.

        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Creator { get; set; } = "None";

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(50)")]
        public string LastModifiedBy { get; set; } = "None";

        public DateTime LastModifyDate { get; set; } = DateTime.Now;
    }
}
