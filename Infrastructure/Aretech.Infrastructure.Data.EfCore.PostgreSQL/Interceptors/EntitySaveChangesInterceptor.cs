using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Interceptors
{
	internal class EntitySaveChangesInterceptor : SaveChangesInterceptor
	{
		public EntitySaveChangesInterceptor()
		{

		}
	}
}
