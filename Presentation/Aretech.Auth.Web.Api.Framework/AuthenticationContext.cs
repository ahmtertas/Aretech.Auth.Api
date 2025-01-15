using Aretech.Core;

namespace Aretech.Auth.Web.Api.Framework
{
	public class AuthenticationContext : IAuthenticationContext
	{
		public Guid AccountId { get; set; }
		public string RegisterNumber { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
	}
}