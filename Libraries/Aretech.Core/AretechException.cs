using System.Net;

namespace Aretech.Core
{
	public class AretechException : Exception
	{
		public int? StatusCode { get; private set; }
		public AretechException() : base() { }
		public AretechException(string message) : base(message) { }
		public AretechException(string message, HttpStatusCode httpStatusCode) : base(message)
		{
			StatusCode = (int)httpStatusCode;
		}
		public AretechException(string message, Exception innerException) : base(message, innerException) { }
	}
}