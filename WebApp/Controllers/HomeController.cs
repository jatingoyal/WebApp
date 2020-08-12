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
        public ActionResult ListFiles(string foldername)
        {
            List<FileModel> files = FileDirectoryService.GetFiles(foldername);
            ViewData["DirFiles"] = files;
            ViewData["Folder"] = foldername;
            return View();
        }

        [Route("Home/ListFolders")]
        [HttpGet]
        public ActionResult ListFolders()
        {
            List<FolderModel> folders = FileDirectoryService.GetAllFolders();
            ViewData["DirFolders"] = folders;
            return View();
        }

        [Route("Home/DownloadFile/{fileId}")]
        [HttpGet]
        public void DownloadFile(int fileId)
        {
            FileDirectoryService.DownloadFile(fileId);
        }
    }
}