using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Digipolis.DataAccess.Entities;
using Digipolis.DataAccess;

namespace Mowei.Entities.Repositories
{
    public class MoweiGenericEntityRepository<TEntity> : MoweiEntityRepositoryBase<Microsoft.EntityFrameworkCore.DbContext, TEntity> where TEntity : EntityBase, new()
    {
		public MoweiGenericEntityRepository(ILogger<DataAccess> logger) : base(logger, null)
		{ }
	}
}