namespace Aretech.Auth.Web.Api.Framework.Models
{
	public class RateLimitingRule
	{
		public string Endpoint { get; set; } // Endpoint (örneğin, "/api/products")
		public string Method { get; set; }   // HTTP metodu (örneğin, "GET", "POST", "*")
		public string Period { get; set; }   // Zaman aralığı (örneğin, "1m", "30s")
		public int Limit { get; set; }       // İzin verilen maksimum istek sayısı
	}
}
