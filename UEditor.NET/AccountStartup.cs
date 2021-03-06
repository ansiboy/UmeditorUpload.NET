﻿using Owin;
using ShopCloud.Common;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace ShopCloud.UserService.Host
{
	internal class AccountStartup
	{
		public class MyAssembliesResolver : IAssembliesResolver
		{
			public virtual ICollection<Assembly> GetAssemblies()
			{
				return new[] { typeof(ShopCloud.UserService.Account.Error).Assembly };
			}
		}

		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration config = AppConfiguration.CreateDefaultInstance();
			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			config.Services.Replace(typeof(IAssembliesResolver), new MyAssembliesResolver());
			appBuilder.UseWebApi(config);
		}
	}
}