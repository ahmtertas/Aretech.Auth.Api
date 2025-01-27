using Aretech.Application.Accounts.Commands.CreateAccount;
using Aretech.Application.Accounts.Commands.Login;
using Aretech.Domain.Accounts;
using Aretech.Services.Accounts.Models;
using AutoMapper;

namespace Aretech.Application.Mapping
{
	public class ApplicationMappingProfile : Profile
	{
		public ApplicationMappingProfile()
		{
			CreateMap<CreateAccountCommand, Account>();
			CreateMap<LoginCommand, LoginModel>();
		}
	}
}