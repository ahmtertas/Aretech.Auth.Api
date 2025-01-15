using System.Reflection;

namespace Aretech.MQ.RabbitMQ.Logging
{
	public class PropertyCryptionService : IPropertyCryptionService
	{
		private readonly ICrytopgraphyService _crytopgraphyService;
		public PropertyCryptionService(ICrytopgraphyService crytopgraphyService)
		{
			_crytopgraphyService = crytopgraphyService;
		}


		public void CryptProperties<T>(T instnace)
		{
			var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (var property in properties)
			{
				if (property.GetCustomAttribute(typeof(IsCrypted)) is not null)
				{
					if (property.CanWrite && property.PropertyType == typeof(string))
					{
						var plainText = property.GetValue(instnace).ToString();
						property.SetValue(instnace, _crytopgraphyService.Encrypt(plainText));
					}
				}
			}

		}

		public void DecryptProperties<T>(T instance)
		{
			var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (var property in properties)
			{
				if (property.GetCustomAttribute(typeof(IsCrypted)) is not null)
				{
					if (property.CanWrite && property.PropertyType == typeof(string))
					{
						var plainText = property.GetValue(instance).ToString();
						property.SetValue(instance, _crytopgraphyService.Decrypt(plainText));
					}
				}
			}
		}
	}
}
