using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        string basePath = @"C:\Users\gjatin\Desktop\Learning\corpus";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("Home/ListFiles/{foldername}")]
        [HttpGet]
        public string ListFiles(string foldername)
        {
            Debug.WriteLine("XX " + foldername + " YY");
            List<FileModel> files = FileDirectoryService.GetFiles(foldername);
            String s="";
            foreach(FileModel f in files)
            {
                s += f.Id + " " + f.Name + "           ";
            }
            return s;
        }

        [Route("Home/ListFolders")]
        [HttpGet]
        public string ListFolders()
        {
            List<FolderModel> folders = FileDirectoryService.GetAllFolders();
            string s = "";
            foreach (FolderModel f in folders)
            {
                s += f.Name + "\n";
            }
            return s;
        }

        [Route("Home/DownloadFile/{fileId}")]
        [HttpGet]
        public void DownloadFile(int fileId)
        {
            /*FileInfo fileInfo = new FileInfo(basePath + "\\" + folder + "\\" + filename);
            if (fileInfo.Exists)
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(fileInfo.FullName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileInfo.Name);
            }
            return StatusCode(403);*/
            FileDirectoryService.DownloadFile(fileId);
            Debug.WriteLine("ZZ"+fileId);
        }
    }
}