using Digipolis.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mowei.Entities.EntityBaseBuilder
{
    public class EntityBaseBuilder<T> where T : class, IEntityBase
    {
        public EntityBaseBuilder(EntityTypeBuilder<T> entityBuilder)
        {
            entityBuilder.Property(e => e.Creator)
                .IsRequired()
                .HasColumnName("Creator")
                .HasDefaultValue("None");

            entityBuilder.Property(e => e.CreateDate)
                .IsRequired()
                .HasColumnName("CreateDate")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            entityBuilder.Property(e => e.LastModifiedBy)
                .IsRequired()
                .HasColumnName("LastModifiedBy")
                .HasDefaultValue("None");

            entityBuilder.Property(e => e.LastModifyDate)
                .IsRequired()
                .HasColumnName("LastModifyDate")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");
        }
    }
}
