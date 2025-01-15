using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Aretech.Auth.Web.Api.Framework.Controllers
{
	[Authorize]
	public class BaseController : ControllerBase
	{
		protected ISender Mediator => HttpContext.RequestServices.GetRequiredService<ISender>();
	}
}