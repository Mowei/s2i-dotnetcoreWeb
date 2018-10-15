using System;
using Digipolis.DataAccess.Context;
using Digipolis.DataAccess.Uow;
using Digipolis.DataAccess.Repositories;
using Digipolis.DataAccess.Paging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Digipolis.DataAccess
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services) where TEntityContext : EntityContextBase<TEntityContext>
        {
            RegisterDataAccess<TEntityContext>(services);
            return services;
        }

        private static void RegisterDataAccess<TEntityContext>(IServiceCollection services) where TEntityContext : EntityContextBase<TEntityContext>
        {
            services.TryAddSingleton<IUowProvider, UowProvider>();
            services.TryAddTransient<IEntityContext, TEntityContext>();
            services.TryAddTransient(typeof(IRepository<>), typeof(GenericEntityRepository<>));
            services.TryAddTransient(typeof(IDataPager<>), typeof(DataPager<>));
        }

        /// <summary>
        /// mowei add for Identity
        /// </summary>
        /// <typeparam name="TEntityContext"></typeparam>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityDataAccess<TEntityContext, TUser, TRole, TKey>(this IServiceCollection services) where TEntityContext : IdentityDbContext<TUser, TRole, TKey>, IEntityContext
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        {
            services.TryAddScoped<IUowProvider, UowProvider>();//TryAddSingleton modify to TryAddScoped
            services.TryAddTransient<IEntityContext, TEntityContext>();
            services.TryAddTransient(typeof(IRepository<>), typeof(GenericEntityRepository<>));
            services.TryAddTransient(typeof(IDataPager<>), typeof(DataPager<>));
            return services;
        }

        private static void ValidateMandatoryField(string field, string fieldName)
        {
            if (field == null) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
            if (field.Trim() == String.Empty) throw new ArgumentException($"{fieldName} cannot be empty.", fieldName);
        }

        private static void ValidateMandatoryField(object field, string fieldName)
        {
            if (field == null) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
        }
    }
}
