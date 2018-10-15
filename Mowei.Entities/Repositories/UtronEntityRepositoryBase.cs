using Digipolis.DataAccess;
using Digipolis.DataAccess.Entities;
using Digipolis.DataAccess.Query;
using Digipolis.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mowei.Entities.Repositories
{
    public abstract class MoweiEntityRepositoryBase<TContext, TEntity> : RepositoryBase<TContext>, IMoweiRepository<TEntity> where TContext : Microsoft.EntityFrameworkCore.DbContext where TEntity : EntityBase, new()
	{
		private readonly OrderBy<TEntity> DefaultOrderBy = new OrderBy<TEntity>(qry => qry.OrderBy(e => e.Id));

		protected MoweiEntityRepositoryBase(ILogger<DataAccess> logger, TContext context) : base(logger, context)
		{ }

		public virtual IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
		{
			var result = QueryDb(null, orderBy, includes);
			return result;
		}

        public virtual void Load(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            result.Load();
        }

        public virtual async Task LoadAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(null, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IQueryable<TEntity> GetPage(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
		{
            if (orderBy == null) orderBy = DefaultOrderBy.Expression;

            var result = QueryDb(null, orderBy, includes);
			return result.Skip(startRow).Take(pageLength);
		}

		public virtual TEntity Get(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
		{
			IQueryable<TEntity> query = Context.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }

            return query.SingleOrDefault(x => x.Id == id);
		}

		public virtual Task<TEntity> GetAsync(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
		{
			IQueryable<TEntity> query = Context.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }

            return query.SingleOrDefaultAsync(x => x.Id == id);
		}

		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
		{
			var result = QueryDb(filter, orderBy, includes);
			return result;
		}

        public virtual void Load(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            result.Load();
        }

        public virtual async Task LoadAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IQueryable<TEntity> QueryPage(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
		{
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (orderBy == null) orderBy = DefaultOrderBy.Expression;

            var result = QueryDb(filter, orderBy, includes);
			return result.Skip(startRow).Take(pageLength);
		}


		public virtual void Add(TEntity entity)
		{
			if ( entity == null ) throw new InvalidOperationException("Unable to add a null entity to the repository.");
			Context.Set<TEntity>().Add(entity);
		}

		public virtual TEntity Update(TEntity entity)
		{
            return Context.Set<TEntity>().Update(entity).Entity;
        }

		public virtual void Remove(TEntity entity)
		{
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Set<TEntity>().Remove(entity);
		}

		public virtual void Remove(Guid id)
		{
			var entity = new TEntity() { Id = id };
			this.Remove(entity);
		}

        public virtual bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Any();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.AnyAsync();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
		{
			IQueryable<TEntity> query = Context.Set<TEntity>();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.Count();
		}

		public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
		{
			IQueryable<TEntity> query = Context.Set<TEntity>();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.CountAsync();
		}

        protected IQueryable<TEntity> QueryDb(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy , Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public void SetUnchanged(TEntity entity)
        {
            base.Context.Entry<TEntity>(entity).State = EntityState.Unchanged;
        }

    }
}