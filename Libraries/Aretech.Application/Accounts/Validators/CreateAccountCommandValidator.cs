using Aretech.Application.Accounts.Commands.CreateAccount;
using FluentValidation;

namespace Aretech.Application.Accounts.Validators
{
	public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
	{
		public CreateAccountCommandValidator()
		{
			RuleFor(x => x.Username)
				.NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
				.MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.");

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("E-posta boş olamaz.")
				.EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Şifre boş olamaz.")
				.MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
				.Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
				.Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
				.Matches("[0-9]").WithMessage("Şifre en az bir sayı içermelidir.")
				.Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

			When(x => !string.IsNullOrEmpty(x.PhoneNumber), () =>
			{
				RuleFor(x => x.PhoneNumber)
					.Length(10).WithMessage("Telefon numarasını başına sıfır koymadan yazınız.")
					.Matches(@"^\+?[0-9]{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz.");
			});

			When(x => !string.IsNullOrEmpty(x.IdentityNumber), () =>
			{
				RuleFor(x => x.IdentityNumber)
					.Length(11).WithMessage("Kimlik numarası 11 karakter olmalıdır.")
					.Matches(@"^[0-9]+$").WithMessage("Kimlik numarası sadece rakamlardan oluşmalıdır.");
			});

			When(x => !string.IsNullOrEmpty(x.FirstName), () =>
			{
				RuleFor(x => x.FirstName)
					.MinimumLength(2).WithMessage("Ad en az 2 karakter olmalıdır.")
					.MaximumLength(50).WithMessage("Ad en fazla 50 karakter olmalıdır.")
					.Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s-]+$").WithMessage("Ad sadece harfler, boşluk ve tire içerebilir.");
			});

			When(x => !string.IsNullOrEmpty(x.LastName), () =>
			{
				RuleFor(x => x.LastName)
					.MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır.")
					.MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olmalıdır.")
					.Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s-]+$").WithMessage("Soyad sadece harfler, boşluk ve tire içerebilir.");
			});
		}
	}
}