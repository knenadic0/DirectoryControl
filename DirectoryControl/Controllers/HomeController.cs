using DirectoryControl.Models;
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

        public ActionResult Index(int? id)
        {
            Directory directory;
            if (id.HasValue)
            {
                directory = service.GetDirectory(id.Value);
            }
            else
            {
                directory = new Directory()
                {
                    Name = "Root",
                    Directories = service.GetDirectories().ToList()
                };
            }

            return View(directory);
        }

        public ActionResult AddNew(string name, int? parent)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index");
            }

            var directory = new Directory
            {
                Name = name,
                Parent = parent
            };
            service.InsertDirectory(directory);

            if (!parent.HasValue)
            {

            }
                return RedirectToAction("Index");
        }
    }
}