using System;
using System.Web.Mvc;
using Utils;

namespace WebApi
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class WebApiAITrackingAttribute : FilterAttribute, IExceptionFilter
	{
		public void OnException(ExceptionContext filterContext)
		{
			if (filterContext != null && filterContext.HttpContext != null && filterContext.Exception != null)
			{
				ApplicationInsightsSettings.TelemetryClient.TrackException(filterContext.Exception);
			}
		}
	}
}
