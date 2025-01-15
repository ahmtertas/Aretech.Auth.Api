using Newtonsoft.Json;
using StackExchange.Redis.Extensions.Newtonsoft;
namespace Aretech.Caching.Redis.Serializers
{
	public class CacheSerializer : NewtonsoftSerializer
	{
		public CacheSerializer() : base(new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All,
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
			ContractResolver = new PrivateResolver()
		})
		{ }
	}
}