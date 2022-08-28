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
                directory = service.GetRootFolder();
            }

            if (directory == null)
            {
                var errorModel = new ErrorModel()
                {
                    Code = 404,
                    Message = "Folder not found"
                };
                return View("Error", errorModel);
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
                var errorModel = new ErrorModel()
                {
                    Code = 400,
                    Message = "Bad request - name cannot be empty"
                };
                return View("Error", errorModel);
            }

            var directory = new Directory
            {
                Name = name,
                Parent = parent.ToNull()
            };

            try
            {
                service.InsertDirectory(directory);
            }
            catch (Exception)
            {
                var errorModel = new ErrorModel()
                {
                    Code = 400,
                    Message = "Bad request - directory with same name already exists in this folder"
                };
                return View("Error", errorModel);
            }

            return RedirectToAction("Folder", new { id = parent.ToNull() });
        }

        [HttpPost]
        public ActionResult Rename(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var errorModel = new ErrorModel()
                {
                    Code = 400,
                    Message = "Bad request - name cannot be empty"
                };
                return View("Error", errorModel);
            }

            var directory = service.GetDirectory(id);
            if (directory != null)
            {
                try
                {
                    directory.Name = name;
                    service.UpdateDirectory(directory);
                }
                catch (Exception)
                {
                    var errorModel = new ErrorModel()
                    {
                        Code = 400,
                        Message = "Bad request - directory with same name already exists in parent folder"
                    };
                    return View("Error", errorModel);
                }
            }

            return RedirectToAction("Folder", new { id });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var directory = service.GetDirectory(id);
            if (directory != null)
            {
                int? parentId = directory.Parent;
                service.DeleteDirectory(id);
                return RedirectToAction("Folder", new { Id = parentId });
            }
            else
            {
                var errorModel = new ErrorModel()
                {
                    Code = 400,
                    Message = $"Bad request - directory with {id} does not exist"
                };
                return View("Error", errorModel);
            }
        }
    }
}