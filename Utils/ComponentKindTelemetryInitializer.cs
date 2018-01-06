using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{

	public class ComponentKindTelemetryInitializer : ITelemetryInitializer
	{
		public void Initialize(ITelemetry telemetry)
		{
			if (!telemetry.Context.Properties.ContainsKey("IsFrontend"))
				telemetry.Context.Properties.Add("IsFrontend", ApplicationInsightsSettings.IsFrontend.ToString());
			if (!telemetry.Context.Properties.ContainsKey("IsBackend"))
				telemetry.Context.Properties.Add("IsBackend", ApplicationInsightsSettings.IsBackend.ToString());
			if (!telemetry.Context.Properties.ContainsKey("IsPublicApi"))
				telemetry.Context.Properties.Add("IsPublicApi", ApplicationInsightsSettings.IsPublicApi.ToString());
		}
	}
}
