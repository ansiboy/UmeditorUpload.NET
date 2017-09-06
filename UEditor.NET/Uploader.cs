using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using UMEditor;


/// <summary>
/// UEditor编辑器通用上传类
/// </summary>
public class Uploader
{
    string state = "SUCCESS";

    public Hashtable upFile(string imageBase64)
    {
        var uploadpath = Path.Combine(Program.uploadPhyscialPath, DateTime.Now.ToString("yyyy-MM-dd"));
		Hashtable infoList = new Hashtable();

		try
        {
            //目录创建
            createFolder(uploadpath);

            ////格式验证
            //if (checkType(filetype))
            //{
            //	state = "不允许的文件类型";
            //}
            ////大小验证
            //if (checkSize(size))
            //{
            //	state = "文件大小超出网站限制";
            //}

            //var imageBase64 = cxt.Request.Form["image"];
            if (string.IsNullOrEmpty(imageBase64))
            {
                state = "Can not get image file form from.";
            }

            var arr = imageBase64.Split(',');
            if (arr.Length != 2)
            {
                state = "image format is error";
            }

            //保存图片
            if (state == "SUCCESS")
            {
                var bytes = Convert.FromBase64String(arr[1]);
                var stream = new MemoryStream(bytes);
                var image = System.Drawing.Image.FromStream(stream);
                var size = bytes.Length;
                var filename = Guid.NewGuid().ToString();
                var pathName = Path.Combine(uploadpath, filename);
                image.Save(pathName);

                var URL = Path.Combine(Program.uploadVirtualPath, filename);

                infoList.Add("url", URL);
                infoList.Add("name", Path.GetFileName(URL));
                infoList.Add("size", size);

            }
        }
        catch (Exception e)
        {
            state = "未知错误";
        }

		infoList.Add("state", state);
		return infoList;

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