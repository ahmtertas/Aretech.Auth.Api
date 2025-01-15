namespace Aretech.Infrastructure.Data
{
	public interface IRepository<TEntity> where TEntity : class
	{
		void Add(TEntity entity);
		Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
		Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
		void Update(TEntity entity);
		void UpdateRange(List<TEntity> entities);
		void Delete(TEntity entity);
		void DeleteRange(List<TEntity> entities);
		IQueryable<TEntity> TableNoTracking { get; }
		IQueryable<TEntity> Table { get; }
		IQueryable<TEntity> TableNoTrackingWithIdentityResolution { get; }
		Task<int> SaveChangesAsync();
		void HardDelete(TEntity entity);
	}
}