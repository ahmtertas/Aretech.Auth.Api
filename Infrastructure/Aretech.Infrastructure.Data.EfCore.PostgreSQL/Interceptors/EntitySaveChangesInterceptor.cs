using Aretech.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Interceptors
{
	internal class EntitySaveChangesInterceptor : SaveChangesInterceptor
	{
		public EntitySaveChangesInterceptor()
		{

		}

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			if (eventData.Context != null)
				ApplyEntityStateChanges(eventData.Context);

			return base.SavingChanges(eventData, result);
		}

		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			if (eventData.Context != null)
				ApplyEntityStateChanges(eventData.Context);

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private void ApplyEntityStateChanges(DbContext dbContext)
		{
			IEnumerable<EntityEntry<BaseIntSoftDeleteEntity>> entries = dbContext.ChangeTracker.Entries<BaseIntSoftDeleteEntity>();
			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added)
				{
					entry.Entity.CreatedDate = DateTime.Now;
					entry.Entity.IsDeleted = false;
					entry.Property(e => e.CreatedDate).IsModified = true;
					entry.Property(e => e.IsDeleted).IsModified = true;
				}

				if (entry.State == EntityState.Deleted)
				{
					entry.State = EntityState.Modified;
					entry.Entity.IsDeleted = true;
					entry.Property(e => e.IsDeleted).IsModified = true;
				}
			}
		}
	}
}
