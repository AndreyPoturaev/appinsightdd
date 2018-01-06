using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Utils
{

	public class DependencyTrackingHandler : HttpClientHandler
	{
		public DependencyTrackingHandler()
		{
		}

		private static int _dependencyCounter = 0;

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
		  CancellationToken cancellationToken)
		{
			using (var op = ApplicationInsightsSettings.TelemetryClient.StartOperation<DependencyTelemetry>(request.Method.Method + " " + request.RequestUri.AbsolutePath))
			{
				op.Telemetry.Target = request.RequestUri.Authority;
				op.Telemetry.Type = request.Method.Method;
				op.Telemetry.Data = request.Method.Method + " " + request.RequestUri.ToString();
				op.Telemetry.Id += "." + Interlocked.Increment(ref _dependencyCounter).ToString();

				request.Headers.Add("Request-Id", op.Telemetry.Id);

				var result = base.SendAsync(request, cancellationToken).Result;
				op.Telemetry.ResultCode = result.StatusCode.ToString();
				op.Telemetry.Success = result.IsSuccessStatusCode;
				return result;
			}
		}
	}
}
