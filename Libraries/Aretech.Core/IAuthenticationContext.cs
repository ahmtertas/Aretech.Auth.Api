namespace Aretech.Core
{
	public interface IAuthenticationContext
	{
		Guid AccountId { get; set; }
		string RegisterNumber { get; set; }
		string? FirstName { get; set; }
		string? LastName { get; set; }
	}
}