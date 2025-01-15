using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Aretech.Caching.Redis.Serializers
{
	public class PrivateResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo memberInfo, MemberSerialization memberSerialization)
		{
			var prop = base.CreateProperty(memberInfo, memberSerialization);
			if (!prop.Writable)
			{
				var property = memberInfo as PropertyInfo;
				var hasPrivateSetter = property?.GetSetMethod(true) != null;
				prop.Writable = hasPrivateSetter;
			}
			return prop;
		}
	}
}