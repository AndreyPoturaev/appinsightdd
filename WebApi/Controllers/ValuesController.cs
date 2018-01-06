using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Utils;

namespace WebApi.Controllers
{
	public class ValuesController : ApiController
	{
		// GET api/values
		public string Get()
		{
			var finalResult = AIInfo.GetAIInfo(HttpContext.Current.GetRequestTelemetry());
			return finalResult;
		}

		// GET api/values/5
		public string Get(int id)
		{
			//throw new ApplicationException("Error message");
			return AIInfo.GetAIInfo(HttpContext.Current.GetRequestTelemetry());
		}

		// POST api/values
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
