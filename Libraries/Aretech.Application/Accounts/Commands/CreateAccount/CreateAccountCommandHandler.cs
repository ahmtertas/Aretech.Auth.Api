using Aretech.Application.Mapping;
using Aretech.Application.SeedWork;
using Aretech.Domain.Accounts;
using Aretech.Services.Accounts.AccountsService;
using MediatR;

namespace Aretech.Application.Accounts.Commands.CreateAccount
{
	public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ApiResponse<CreateAccountResponse>>
	{
		private readonly IAccountService _accountService;
		private readonly IMappingService _mappingService;
		public CreateAccountCommandHandler
		(
						 IAccountService accountService,
						 IMappingService mappingService
		)
		{
			_accountService = accountService;
			_mappingService = mappingService;
		}
		public async Task<ApiResponse<CreateAccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			var account = _mappingService.Map<CreateAccountCommand, Account>(request);
			await _accountService.AddAsync(account: account);

			return new ApiResponse<CreateAccountResponse>()
			{
				Success = true,
				Message = "",
				Data = new CreateAccountResponse()
			};
		}
	}
}