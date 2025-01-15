using System.Net;

namespace Aretech.Application.SeedWork
{
	public class BaseEndPointResponse<T> where T : class
	{
		public T Data { get; set; }
		public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
		public string Message { get; set; } = string.Empty;

		public List<string> ErrorMessage { get; set; }
		public BaseEndPointResponse()
		{
			ErrorMessage = new List<string>();
		}

		public BaseEndPointResponse<T> AddError(string message)
		{
			ErrorMessage.Add(message);
			return this;
		}
	}
}