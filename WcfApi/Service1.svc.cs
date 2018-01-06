using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.ApplicationInsights.Wcf;
using System.Text;
using Utils;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights;

namespace WcfApi
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
	//[WcfAITracking]
	[ServiceTelemetry]
	public class Service1 : IService1
	{
		public string GetData(int value)
		{
			if (CheckCorsPreFlight())
				return null;

			using (var client = new HttpClient(new DependencyTrackingHandler()))
			{
				var result = client.GetStringAsync($"http://localhost:32149/api/values/{value}").Result;

				var finalResult = AIInfo.GetAIInfo(OperationContext.Current.GetRequestTelemetry()) + Environment.NewLine + result;
				return finalResult;
			}
		}

		private bool CheckCorsPreFlight()
		{
			var cors = false;
			if (WebOperationContext.Current != null)
			{
				var request = WebOperationContext.Current.IncomingRequest;
				var response = WebOperationContext.Current.OutgoingResponse;

				if (request.Method == "OPTIONS")
				{
					response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
					response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-Requested-With, x-ms-request-root-id, x-ms-request-id");
					response.Headers.Add("Access-Control-Allow-Credentials", "true");
					cors = true;
				}
				var origin = request.Headers.GetValues("Origin");
				if (origin.Length > 0)
					response.Headers.Add("Access-Control-Allow-Origin", origin[0]);
				else
					response.Headers.Add("Access-Control-Allow-Origin", "*");
			}
			return cors;
		}
	}
}
