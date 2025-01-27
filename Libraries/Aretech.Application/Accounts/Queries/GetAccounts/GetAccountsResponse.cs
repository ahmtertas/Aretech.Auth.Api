namespace Aretech.Application.Accounts.Queries.GetAccounts
{
	public class GetAccountsResponse
	{
		public string Username { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string? PhoneNumber { get; set; } = null!;
		public string? IdentityNumber { get; set; } = null!;
		public string? FirstName { get; set; } = null!;
		public string? LastName { get; set; } = null!;
		public DateTime CreatedDate { get; set; }
		public Guid CreatedBy { get; set; }
		public string CreatedByName { get; set; }
	}
}