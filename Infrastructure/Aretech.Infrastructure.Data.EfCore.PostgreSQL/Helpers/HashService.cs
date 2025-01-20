using System.Security.Cryptography;

namespace Aretech.Infrastructure.Data.EfCore.PostgreSQL.Helpers
{
	public class HashService : IHashService
	{
		private const int SaltSize = 16;
		private const int HashSize = 32;
		private const int Iterations = 10000;

		public string HashPassword(string password)
		{
			byte[] salt = new byte[SaltSize];
			using (var rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(salt);
			}

			using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
			{
				byte[] hash = pbkdf2.GetBytes(HashSize);
				byte[] hashBytes = new byte[SaltSize + HashSize];
				Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
				Buffer.BlockCopy(hash, 0, hashBytes, SaltSize, HashSize);

				return Convert.ToBase64String(hashBytes);
			}
		}

		public bool VerifyPassword(string enteredPassword, string storedHash)
		{
			byte[] hashBytes = Convert.FromBase64String(storedHash);
			byte[] salt = new byte[SaltSize];
			Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);

			using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations))
			{
				byte[] hash = pbkdf2.GetBytes(HashSize);
				for (int i = 0; i < HashSize; i++)
				{
					if (hashBytes[i + SaltSize] != hash[i])
					{
						return false;
					}
				}
				return true;
			}
		}
	}
}