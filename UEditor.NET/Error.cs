using System;
namespace UMEditor
{
	public class Error
	{
		public Error()
		{
		}

		class AppSettingItemMissException : Exception
		{
			public AppSettingItemMissException(string name)
				: base(GetMessage(name))
			{

			}

			static string GetMessage(string name)
			{
				var msg = string.Format("The app setting item '{0}' is not config.", name);
				return msg;
			}
		}

		internal static Exception AppSettingItemMiss(string name)
		{
			return new AppSettingItemMissException(name);
		}
	}
}
