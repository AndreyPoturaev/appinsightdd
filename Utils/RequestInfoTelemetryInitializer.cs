using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utils
{
	public class RequestInfoTelemetryInitializer : ITelemetryInitializer
	{
		public void Initialize(ITelemetry telemetry)
		{
			var requestTelemetry = telemetry as RequestTelemetry;
			if (HttpContext.Current == null)
				return;
			if (requestTelemetry == null)
				return;

			var request = HttpContext.Current.Request;
			if (!requestTelemetry.Properties.ContainsKey("query"))
			{
				if (request.HttpMethod == HttpMethod.Post.ToString() || request.HttpMethod == HttpMethod.Put.ToString())
				{
					using (var reader = new StreamReader(request.InputStream))
					{
						var position = request.InputStream.Position;
						request.InputStream.Seek(0, SeekOrigin.Begin);
						string requestBody = reader.ReadToEnd();
						request.InputStream.Seek(position, SeekOrigin.Begin);
						requestTelemetry.Properties.Add("query", requestBody);
					}
				}
				if (request.HttpMethod == HttpMethod.Get.ToString())
				{
					var dic = ToDictionary(request.Url.ParseQueryString());

					var requestBody = JsonConvert.SerializeObject(dic);
					requestTelemetry.Properties.Add("query", requestBody);
				}
			}
			if (!requestTelemetry.Properties.ContainsKey("headers"))
			{
				var dic = ToDictionary(request.Headers);
				var requestHeaders = JsonConvert.SerializeObject(dic);
				requestTelemetry.Properties.Add("headers", requestHeaders);
			}
		}

		private static Dictionary<string, string> ToDictionary(NameValueCollection headers)
		{
			var dict = new Dictionary<string, string>();
			foreach (var item in headers.AllKeys)
			{
				var value = headers.GetValues(item);
				if (value != null)
				{
					var header = String.Empty;
					foreach (var v in value)
					{
						header += System.Net.WebUtility.UrlDecode(v) + " ";
					}
					header = header.TrimEnd(" ".ToCharArray());
					dict.Add(item, header);
				}
			}
			return dict;
		}
	}
}
