namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Helpers
{
	public interface IHashService
	{
		string HashPassword(string password);
		bool VerifyPassword(string enteredPassword, string storedHash);
	}
}
