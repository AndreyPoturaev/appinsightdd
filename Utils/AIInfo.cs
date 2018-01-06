using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
	public class AIInfo
	{
		public static string GetAIInfo(RequestTelemetry t)
		{
			var info = "[{0} {1} on {2}] Operation Id '{3}'";
			return string.Format(info,
				ApplicationInsightsSettings.CloudRole,
				ApplicationInsightsSettings.Version,
				ApplicationInsightsSettings.RoleInstance,
				t.Context.Operation.Id
				);
			
		}
	}
}
