using Digipolis.DataAccess.Context;
using Digipolis.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mowei.Entities.EntityBaseBuilder;
using Mowei.Entities.Models;

namespace Mowei.Entities.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IEntityContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor contextAccessor) : base(options)
        {
            _contextAccessor = contextAccessor;
        }

        #region SaveChanges
        public void OnChange()
        {
            var changeSet = ChangeTracker.Entries<IEntityBase>();
            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State == EntityState.Added))
                {
                    entry.Entity.CreateDate = DateTime.Now;
                    entry.Entity.Creator = _contextAccessor.HttpContext.User.Identity.Name ?? "None";
                }
                foreach (var entry in changeSet.Where(c => c.State == EntityState.Modified))
                {
                    entry.Entity.LastModifyDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = _contextAccessor.HttpContext.User.Identity.Name ?? "None";
                }
            }
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnChange();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnChange();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        #endregion

        public virtual DbSet<Project> Project { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            new EntityBaseBuilder<ApplicationUser>(builder.Entity<ApplicationUser>());
            new EntityBaseBuilder<ApplicationRole>(builder.Entity<ApplicationRole>());
            new EntityBaseBuilder<Project>(builder.Entity<Project>());
        }
    }
}
