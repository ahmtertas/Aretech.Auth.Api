namespace Aretech.Core
{
	public class AretechException : Exception
	{
		public AretechException() : base() { }
		public AretechException(string message) : base(message) { }
		public AretechException(string message, Exception innerException) : base(message, innerException) { }
	}
}