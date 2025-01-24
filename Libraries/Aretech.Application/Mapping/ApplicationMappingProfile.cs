using Aretech.Application.Accounts.Commands.CreateAccount;
using Aretech.Domain.Accounts;
using AutoMapper;

namespace Aretech.Application.Mapping
{
	public class ApplicationMappingProfile : Profile
	{
		public ApplicationMappingProfile()
		{
			CreateMap<CreateAccountCommand, Account>();
		}
	}
}