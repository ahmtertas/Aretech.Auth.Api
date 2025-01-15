using Microsoft.EntityFrameworkCore.Storage;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AretechDbContext _dbContext;
		private IDbContextTransaction _dbContextTransaction;
		public UnitOfWork(AretechDbContext aretechDbContext)
		{
			_dbContext = aretechDbContext;
		}
		public async Task BeginTransactionAsync()
		{
			_dbContextTransaction = await _dbContext.Database.BeginTransactionAsync();
		}

		public async Task CommitTransactionAsync()
		{
			await _dbContext.Database.CommitTransactionAsync();
		}

		public async Task RollbackAsync()
		{
			await _dbContext.Database.RollbackTransactionAsync();
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public void SetDeleted<TEntity>(TEntity entity)
		{
			_dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
		}
	}
}