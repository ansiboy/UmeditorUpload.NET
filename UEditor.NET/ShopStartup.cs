using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace ShopCloud.UserService.Host
{
    public class ShopStartup
	{
		public ShopStartup()
		{
			Shop.Startup.Start();
		}

		public class MyAssembliesResolver : IAssembliesResolver
		{
			public virtual ICollection<Assembly> GetAssemblies()
			{
				return new[] { typeof(Shop.Controllers.HomeController).Assembly };
			}
		}

		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration config = AppConfiguration.CreateDefaultInstance();
			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
			//config.Formatters.Add(new BrowserJsonFormatter());

			config.Services.Replace(typeof(IAssembliesResolver), new MyAssembliesResolver());
			appBuilder.UseWebApi(config);
		}

		//public class BrowserJsonFormatter : JsonMediaTypeFormatter
		//{
		//	public BrowserJsonFormatter()
		//	{
		//		this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
		//		this.SerializerSettings.Formatting = Formatting.Indented;
		//	}

		//	public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		//	{
		//		base.SetDefaultContentHeaders(type, headers, mediaType);
		//		headers.ContentType = new MediaTypeHeaderValue("application/json");
		//	}

		//}
	}
}
