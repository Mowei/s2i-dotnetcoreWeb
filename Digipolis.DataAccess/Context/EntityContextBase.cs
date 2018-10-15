using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Digipolis.DataAccess.Context
{
    public class EntityContextBase<TContext> : DbContext, IEntityContext where TContext : DbContext
    {
        public EntityContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }
    }
}
