using Aretech.Application.Common;
using Aretech.Application.Mapping;
using Aretech.Application.SeedWork;
using Aretech.Services.Accounts.AccountsService;
using Aretech.Services.Accounts.Models;
using MediatR;

namespace Aretech.Application.Accounts.Commands.Login
{
	public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginResponse>>
	{
		private readonly IAccountService _accountService;
		private readonly IMappingService _mappingService;
		public LoginCommandHandler
			(
						 IAccountService accountService,
						 IMappingService mappingService
			)
		{
			_accountService = accountService;
			_mappingService = mappingService;
		}
		public async Task<ApiResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(nameof(request), "LoginCommand request is null.");

			var loginModel = _mappingService.Map<LoginCommand, LoginModel>(request);
			var token = await _accountService.LoginAsync(loginModel);

			return new ApiResponse<LoginResponse>()
			{
				Message = MessageConstant.Success,
				Success = true,
				Data = new LoginResponse()
				{
					Token = token
				},
			};
		}
	}
}