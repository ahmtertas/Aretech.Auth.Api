using System.Security.Cryptography;
using System.Text;

namespace Aretech.MQ.RabbitMQ.Logging
{
	internal class CrytopgraphyService : ICrytopgraphyService
	{
		private static readonly string Salt = "AretechConstantKEY_SALT_3456897";
		private static readonly string Password = "AretechConstantKEY_PASSWORD_3456897";

		public string Encrypt(string cipherText)
		{
			using (var aes = Aes.Create())
			{
				var key = GenerateKey(Password, Salt);
				aes.Key = key;
				aes.IV = GenerateInitializationVector();
				using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
				using (var ms = new MemoryStream())
				{
					ms.Write(aes.IV, 0, aes.IV.Length);

					using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
					using (var sw = new StreamWriter(cs))
					{
						sw.Write(cipherText);
					}

					return Convert.ToBase64String(ms.ToArray());
				}
			}
		}

		public string Decrypt(string cipherText)
		{
			var cipherBytes = Convert.FromBase64String(cipherText);

			using (var aes = Aes.Create())
			{
				var key = GenerateKey(password: Password, salt: Salt);
				aes.Key = key;

				using (var ms = new MemoryStream(cipherBytes))
				{
					var iv = new byte[16];
					ms.Read(iv, 0, iv.Length);
					aes.IV = iv;

					using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
					using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
					using (var sr = new StreamReader(cs))
					{
						return sr.ReadToEnd();
					}
				}
			}
		}

		private static byte[] GenerateKey(string password, string salt)
		{
			using (var keyGenerator = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000))
			{
				return keyGenerator.GetBytes(32);
			}
		}

		private static byte[] GenerateInitializationVector()
		{
			using (var aes = Aes.Create())
			{
				aes.GenerateIV();
				return aes.IV;
			}
		}
	}
}