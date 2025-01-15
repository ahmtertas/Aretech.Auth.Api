namespace Aretech.MQ.RabbitMQ.Logging
{
	public interface IPropertyCryptionService
	{
		void CryptProperties<T>(T instnace);
		void DecryptProperties<T>(T instnace);
	}
}
