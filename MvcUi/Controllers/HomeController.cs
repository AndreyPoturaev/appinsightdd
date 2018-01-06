using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Utils;

namespace MvcUi.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			using (var client = new HttpClient(new DependencyTrackingHandler()))
			{
				var result = client.GetStringAsync($"http://localhost:32149/api/values").Result;
				var finalResult = AIInfo.GetAIInfo(HttpContext.GetRequestTelemetry()) + Environment.NewLine + result;

				return View((object)finalResult);
			}
		}



		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		[HttpPost]
		public ActionResult GetAjaxValue()
		{
			using (var client = new HttpClient(new DependencyTrackingHandler()))
			{
				var result = client.GetStringAsync($"http://localhost:32149/api/values/88").Result;
				var finalResult = AIInfo.GetAIInfo(HttpContext.GetRequestTelemetry()) + Environment.NewLine + result;

				return Json(finalResult);
			}
		}
	}
}