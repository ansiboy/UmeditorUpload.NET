using System;
using System.Net.Http.Headers;
using System.Web.Http;
using Owin;

namespace UMEditor
{
    public class Startup
    {
		public void Configuration(IAppBuilder appBuilder)
		{
			// Configure Web API for self-host. 
			HttpConfiguration config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "{controller}/{action}"
			);
			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			appBuilder.UseWebApi(config);
		}
	}
}
