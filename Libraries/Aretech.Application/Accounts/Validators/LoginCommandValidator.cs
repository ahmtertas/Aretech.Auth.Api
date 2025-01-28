using Aretech.Application.Accounts.Commands.Login;
using FluentValidation;

namespace Aretech.Application.Accounts.Validators
{
	public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("Kullanıcı adı boş olamaz.");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Şifre boş olamaz.");
		}
	}
}