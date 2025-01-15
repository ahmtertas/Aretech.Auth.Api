namespace Aretech.MQ.RabbitMQ.Logging
{
	public interface ICrytopgraphyService
	{
		string Encrypt(string plainText);
		string Decrypt(string cipherText);
	}
}
