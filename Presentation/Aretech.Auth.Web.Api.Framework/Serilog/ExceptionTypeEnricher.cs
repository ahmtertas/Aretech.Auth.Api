using Serilog.Core;
using Serilog.Events;

namespace Aretech.Auth.Web.Api.Framework.Serilog
{
	public class ExceptionTypeEnricher : ILogEventEnricher
	{
		public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
		{
			if (logEvent.Exception != null)
			{
				var exceptionType = logEvent.Exception.GetType().FullName;
				var exceptionTpeProperty = propertyFactory.CreateProperty("ExceptionType", exceptionType);
				logEvent.AddPropertyIfAbsent(exceptionTpeProperty);
			}
		}
	}
}