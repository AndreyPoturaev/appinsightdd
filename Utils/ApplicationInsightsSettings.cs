using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Reflection;
using System.Threading;

namespace Utils
{
	public static class ApplicationInsightsSettings
	{
		private static string _cloudRole;
		private static TelemetryClient _telemetryClient;

		public static Assembly MainAssembly { get; set; }
		public static string CloudRole
		{
			get
			{
				if (MainAssembly != null && _cloudRole == null)
					_cloudRole = MainAssembly.GetName().Name;
				return _cloudRole ?? "";
			}
			set => _cloudRole = value;
		}

		public static string Version
		{
			get
			{
				return MainAssembly?.GetName().Version.ToString() ?? "";
			}
		}

		public static string RoleInstance { get => Environment.MachineName; }

		public static bool IsBackend { get; set; }
		public static bool IsFrontend { get; set; }
		public static bool IsPublicApi { get; set; }

		public static TelemetryClient TelemetryClient
		{
			get => _telemetryClient ?? (_telemetryClient = new TelemetryClient());
		}
		public static TelemetryConfiguration Configuration { get => TelemetryConfiguration.Active; }

		public static string InstrumentationKey
		{
			get => System.Configuration.ConfigurationManager.AppSettings["AppInsightKey"];
		}
	}

}