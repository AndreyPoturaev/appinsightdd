using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utils;

namespace WcfApi.App_Code
{
	public class Initializer
	{
		public static void AppInitialize()
		{
			ApplicationInsightsSettings.MainAssembly = typeof(IService1).Assembly;
			ApplicationInsightsSettings.IsBackend = true;

			TelemetryConfiguration.Active.InstrumentationKey = ApplicationInsightsSettings.InstrumentationKey;
		}
	}

	
}