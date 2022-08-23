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

        public IEnumerable<Directory> GetDirectories()
        {
            return repository.GetDirectories();
        }

        public IEnumerable<Directory> GetRootDirectories()
        {
            return repository.GetDirectories().Where(x => !x.Parent.HasValue).OrderBy(x => x.Name);
        }

        public IEnumerable<Directory> GetDirectories(int id)
        {
            return repository.GetDirectories().Where(x => x.Parent == id).OrderBy(x => x.Name);
        }

        public Directory GetDirectory(int id)
        {
            return repository.GetDirectory(id);
        }

        public void InsertDirectory(Directory directory)
        {
            repository.InsertDirectory(directory);
            repository.Save();
        }

        public void DeleteDirectory(int directoryId)
        {
            repository.DeleteDirectory(directoryId);
            repository.Save();
        }

        public void UpdateDirectory(Directory directory)
        {
            repository.UpdateDirectory(directory);
            repository.Save();
        }
    }
}