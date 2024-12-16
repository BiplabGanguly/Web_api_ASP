using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Customer_WEB_API
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{

			EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");

			config.MapHttpAttributeRoutes();
			config.EnableCors(cors);
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
