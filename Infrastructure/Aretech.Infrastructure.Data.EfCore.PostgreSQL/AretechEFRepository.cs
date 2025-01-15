using Microsoft.EntityFrameworkCore;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL
{
	public class AretechEFRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly AretechDbContext _context;
		private DbSet<TEntity> _dbSet;


		public AretechEFRepository(AretechDbContext dbContext)
		{
			_context = dbContext;
			_dbSet = dbContext.Set<TEntity>();
		}

		public IQueryable<TEntity> TableNoTracking => _dbSet.AsNoTracking();


		public IQueryable<TEntity> Table => _dbSet;

		public IQueryable<TEntity> TableNoTrackingWithIdentityResolution => _dbSet.AsNoTrackingWithIdentityResolution();

		public void Add(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			await _dbSet.AddAsync(entity, cancellationToken);
		}

		public async Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
		{
			await _dbSet.AddRangeAsync(entities, cancellationToken);

		}

		public void Delete(TEntity entity)
		{
			_dbSet.Update(entity);
		}

		public void DeleteRange(List<TEntity> entities)
		{
			_dbSet.UpdateRange(entities);
		}

		public void HardDelete(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Update(TEntity entity)
		{
			_dbSet.Update(entity);
		}

		public void UpdateRange(List<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				Update(entity);
			}
		}
	}
}