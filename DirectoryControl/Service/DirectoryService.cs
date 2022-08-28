using DirectoryControl.Common;
using DirectoryControl.Models;
using DirectoryControl.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DirectoryControl.Service
{
    public class DirectoryService
    {
        private DirectoryRepository repository;

        public DirectoryService()
        {
            repository = new DirectoryRepository(new dbEntities());
        }

        public IEnumerable<Directory> GetRootDirectories()
        {
            return repository.GetDirectories().Where(x => !x.Parent.HasValue).OrderBy(x => x.Name);
        }

        public Directory GetDirectory(int? id = null)
        {
            return id.HasValue ? repository.GetDirectory(id.Value) : new Directory()
            {
                Name = "Root",
                Id = 0,
                Directories = GetRootDirectories().ToList()
            };
        }

        public void InsertDirectory(Directory directory)
        {
            repository.InsertDirectory(directory);
            repository.Save();
        }

        public void DeleteDirectory(int directoryId)
        {
            var directory = repository.GetDirectory(directoryId);

            foreach (var subfolder in Helper.Collect(directory.Directories.ToList()))
            {
                repository.DeleteDirectory(subfolder);
            }
            repository.DeleteDirectory(directory);

            repository.Save();
        }

        public void UpdateDirectory(Directory directory)
        {
            repository.UpdateDirectory(directory);
            repository.Save();
        }
    }
}