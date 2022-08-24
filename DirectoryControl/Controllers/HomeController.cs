using DirectoryControl.Common;
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

        public ActionResult Folder(int? id = null)
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
                    Id = 0,
                    Directories = service.GetDirectories().ToList()
                };
            }

            return View(directory);
        }

        public ActionResult AddNew(string name, int? parent)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Folder");
            }

            var directory = new Directory
            {
                Name = name,
                Parent = parent.ToNull()
            };
            service.InsertDirectory(directory);

            return RedirectToAction("Folder", new { id = parent.ToNull() });
        }
    }
}