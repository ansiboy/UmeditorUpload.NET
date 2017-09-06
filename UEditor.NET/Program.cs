using Microsoft.Owin.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Configuration;
using System.Net.Http;

namespace UMEditor
{
    class Program
    {
        public static string uploadPhyscialPath;
        public static string uploadVirtualPath;
        static void Main(string[] args)
        {
            var bindingAddressesString = ConfigurationManager.AppSettings["bindingAddresses"];
            if (string.IsNullOrEmpty(bindingAddressesString))
                throw Error.AppSettingItemMiss("bindingAddresses");

            var portString = ConfigurationManager.AppSettings["port"];
            if (string.IsNullOrEmpty(portString))
                throw Error.AppSettingItemMiss("port");

            uploadPhyscialPath = ConfigurationManager.AppSettings["uploadPhyscialPath"];
            if (string.IsNullOrEmpty(uploadPhyscialPath))
                throw Error.AppSettingItemMiss("uploadPhyscialPath");

            uploadVirtualPath = ConfigurationManager.AppSettings["uploadVirtualPath"];
            if (string.IsNullOrEmpty(uploadVirtualPath))
                throw Error.AppSettingItemMiss("uploadVirtualPath");

            var port = Convert.ToInt32(portString);
            var bindingAddresses = bindingAddressesString.Split(',').Select(o => o.Trim()).ToArray();
            var options = new StartOptions();
            for (var i = 0; i < bindingAddresses.Length; i++)
            {
                options.Urls.Add(string.Format("http://{0}:{1}", bindingAddresses[i], port));
            }

            // Start OWIN host 
            using (WebApp.Start<Startup>(options))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                var response = client.GetAsync(string.Format("http://{0}:{1}/values", bindingAddresses[0], port)).Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }
        }

    }
}
