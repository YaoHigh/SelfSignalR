using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SelfSignalR2._0.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat(string name)
        {
            Session["userid"] = Guid.NewGuid().ToString().ToUpper();
            Session["username"] = name;
            return View();
        }

        [HttpPost]
        public ActionResult ImgUpload()
        {
            try
            {
                System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
                System.Web.HttpPostedFile uploadFile = request.Files[0];
                // 文件上传后的保存路径
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";//图片名称
                // img-path images路径--Start
                string filePathDate = DateTime.Now.ToShortDateString().ToString();
                filePathDate = filePathDate.Replace("-", "/");
                // img-path images路径--End
                string imagePath = "/image/" + "/" + filePathDate + "/";
                string filepath = Server.MapPath("~/upload") + imagePath;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                uploadFile.SaveAs(filepath + filename);
                //HttpPostedFileBase File1 = Request.Files[0];
                //检查上传的物理路径是否存在，不存在则创建
                string imgurl = HostUrl() + "/upload" + imagePath + filename;
                return Json(new { success = true, imgurl = imgurl });
            }
            catch (Exception ex)
            {
                return Json(new { success = true, msg = ex.Message });
            }

        }

        #region 当前域名
        /// <summary>
        /// 当前域名
        /// </summary>
        public string HostUrl()
        {
            string hostUrl = "";
            if (HttpContext.Request.Url.Port != 80)
            {
                hostUrl = "http://" + HttpContext.Request.Url.Host + ":" + HttpContext.Request.Url.Port + "/";
            }
            else
            {
                hostUrl = "http://" + HttpContext.Request.Url.Host + "/";
            }

            return hostUrl;
        }
        #endregion
    }
}