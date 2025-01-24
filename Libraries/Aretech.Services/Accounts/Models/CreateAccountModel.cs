namespace Aretech.Services.Accounts.Models
{
	public class CreateAccountModel
	{
		public string Username { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string? PhoneNumber { get; set; } = null!;
		public string? IdentityNumber { get; set; } = null!;
		public string? FirstName { get; set; } = null!;
		public string? LastName { get; set; } = null!;
	}
}