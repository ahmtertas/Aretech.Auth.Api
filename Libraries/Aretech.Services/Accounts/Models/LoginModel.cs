namespace Aretech.Services.Accounts.Models
{
	public record LoginModel
	{
		public string UserName { get; set; } = null!;
		public string Password { get; set; } = null!;
	}
}