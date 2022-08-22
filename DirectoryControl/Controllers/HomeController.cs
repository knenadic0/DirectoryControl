using DirectoryControl.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DirectoryControl.Controllers
{
    public class HomeController : Controller
    {
        private DirectoryService service;

        public HomeController()
        {
            service = new DirectoryService();
        }

        public ActionResult Index()
        {
            var directories = service.GetRootDirectories();
            return View(directories);
        }
    }
}