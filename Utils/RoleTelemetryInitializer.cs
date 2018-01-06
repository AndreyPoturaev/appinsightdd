using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utils
{
	public class RoleTelemetryInitializer : ITelemetryInitializer
	{
		public void Initialize(ITelemetry telemetry)
		{
			telemetry.Context.Cloud.RoleName = ApplicationInsightsSettings.CloudRole;
			telemetry.Context.Component.Version = ApplicationInsightsSettings.Version;
			telemetry.Context.Cloud.RoleInstance = ApplicationInsightsSettings.RoleInstance;

			var identity = Thread.CurrentPrincipal?.Identity;
			if (identity != null && identity.IsAuthenticated)
				telemetry.Context.User.AuthenticatedUserId = identity.Name;
		}
	}
}