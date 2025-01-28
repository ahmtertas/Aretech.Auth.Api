using Aretech.Application.Accounts.Commands.RefreshToken;
using FluentValidation;

namespace Aretech.Application.Accounts.Validators
{
	public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
	{
		public RefreshTokenCommandValidator()
		{
			RuleFor(x => x.AccessToken)
				.NotEmpty().WithMessage("AccessToken boş olamaz.");
			RuleFor(x => x.RefreshToken)
				.NotEmpty().WithMessage("RefreshToken boş olamaz.");
		}
	}
}