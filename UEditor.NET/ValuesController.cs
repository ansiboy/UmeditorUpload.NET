using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace UMEditor
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
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

        [HttpGet]
        public object temp()
        {
            return new object();
        }

        [HttpGet]
        [HttpPost]
        public async Task<HttpResponseMessage> ImageUp()
        {
            var contentStream = await Request.Content.ReadAsStreamAsync();
            var reader = new StreamReader(contentStream);
            var str = reader.ReadToEnd();
            var dic = HttpUtility.ParseQueryString(str);

            var todayDir = DateTime.Now.ToString("yyyy-MM-dd");
            string imageBase64 = dic["image"];
            //string pathbase = Path.Combine(Program.uploadPhyscialPath, DateTime.Now.ToString("yyyy-MM-dd"));
            string state = "SUCCESS";

            var uploadPhycialPath = Path.Combine(Program.uploadPhyscialPath, todayDir);
            createFolder(uploadPhycialPath);

            if (string.IsNullOrEmpty(imageBase64))
                state = "Can not get image file form from.";

            var arr = imageBase64.Split(',');
            if (arr.Length != 2)
                state = "image format is error";


            //保存图片
            object result;
            if (state == "SUCCESS")
            {
                var bytes = Convert.FromBase64String(arr[1]);
                var stream = new MemoryStream(bytes);
                var image = System.Drawing.Image.FromStream(stream);
                var size = bytes.Length;

                var fileName = Guid.NewGuid().ToString();
                var pathName = Path.Combine(uploadPhycialPath, fileName);
                image.Save(pathName);

                var url = Path.Combine(Program.uploadVirtualPath, todayDir, fileName);
                result = new { state, url, size };
            }
            else
            {
                result = new { state };
            }

            var response = new HttpResponseMessage();
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            var s = new System.Web.Script.Serialization.JavaScriptSerializer();
            var str1 = s.Serialize(result);
            response.Content = new StringContent(str1);
            return response;
        }

        /**
 * 按照日期自动创建存储文件夹
 */
        private void createFolder(string uploadpath)
        {
            if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
        }
    }
}
