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

        [HttpGet]
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

        [HttpGet]
        public ActionResult Structure()
        {
            var directory = new Directory()
            {
                Name = "Root",
                Id = 0,
                Directories = service.GetDirectories().ToList()
            };

            return View(directory);
        }

        [HttpPost]
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

        [HttpPost]
        public ActionResult Rename(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Folder");
            }

            var directory = service.GetDirectory(id);
            if (directory != null)
            {
                directory.Name = name;
                service.UpdateDirectory(directory);
            }

            return RedirectToAction("Folder", new { id });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var directory = service.GetDirectory(id);
            if (directory != null)
            {
                service.DeleteDirectory(id);
            }

            return RedirectToAction("Folder");
        }
    }
}