using Aretech.MQ.RabbitMQ.Concrete;
using MongoDB.Driver;

namespace Aretech.MQ.RabbitMQ.Logging
{
	internal class MongoDbService : IDisposable
	{

		private IMongoDatabase _database;
		private IMongoClient _client;
		private readonly IPropertyCryptionService _propertyEncryptionService;
		private readonly RabbitMQLoggingConfiguration _configuration;

		public MongoDbService(IPropertyCryptionService propertyCryptionService, RabbitMQLoggingConfiguration configuration)
		{
			_propertyEncryptionService = propertyCryptionService;
			_configuration = configuration;
			CreateConnection(_configuration.LoggingConnectionString, _configuration.LoggingDatabaseName);
		}

		private void CreateConnection(string connectionString, string databaseName)
		{
			if (_configuration.LoggingEnabled)
			{
				_client = new MongoClient(connectionString);
				_database = _client.GetDatabase(databaseName);
			}
		}

		public async Task AddAsync<T>(T log)
		{
			_propertyEncryptionService.CryptProperties(log);
			var collectionName = !string.IsNullOrEmpty(_configuration.LoggingCollectionName) ? _configuration.LoggingCollectionName : nameof(T);
			var collection = _database.GetCollection<T>(collectionName);
			await collection.InsertOneAsync(log);
		}

		public async Task AddRangeAsync<T>(IEnumerable<T> logs)
		{
			foreach (var log in logs)
			{
				_propertyEncryptionService.CryptProperties(log);
			}

			var collectionName = !string.IsNullOrEmpty(_configuration.LoggingCollectionName) ? _configuration.LoggingCollectionName : nameof(T);
			var collection = _database.GetCollection<T>(collectionName);
			await collection.InsertManyAsync(logs);
		}


		public async Task<T> GetByCorrelationId<T>(string correlationId)
		{
			var collectionName = !string.IsNullOrEmpty(_configuration.LoggingCollectionName) ? _configuration.LoggingCollectionName : nameof(T);
			var collection = _database.GetCollection<T>(collectionName);
			var filter = Builders<T>.Filter.Eq("CorrelationId", correlationId);
			var result = await collection.Find(filter).FirstOrDefaultAsync();
			_propertyEncryptionService.DecryptProperties(result);
			return result;
		}

		public async Task UpdateByCorrelationId<T>(string correlationId, T updatedLog)
		{
			_propertyEncryptionService.CryptProperties(updatedLog);
			var collectionName = !string.IsNullOrEmpty(_configuration.LoggingCollectionName) ? _configuration.LoggingCollectionName : nameof(T);
			var collection = _database.GetCollection<T>(collectionName);
			var filter = Builders<T>.Filter.Eq("CorrelationId", correlationId);

			await collection.ReplaceOneAsync(filter, updatedLog);
		}

		public void CloseConnection()
		{
			if (_client != null)
				_client = null;
		}


		public void Dispose()
		{
			CloseConnection();
		}


	}
}
