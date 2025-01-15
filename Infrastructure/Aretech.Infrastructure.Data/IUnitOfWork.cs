namespace Aretech.Infrastructure.Data
{
	public interface IUnitOfWork
	{
		Task BeginTransactionAsync();
		Task CommitTransactionAsync();
		Task RollbackAsync();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		void SetDeleted<TEntity>(TEntity entity);
	}
}