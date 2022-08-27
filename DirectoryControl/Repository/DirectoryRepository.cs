using DirectoryControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DirectoryControl.Repository
{
    public class DirectoryRepository : IDisposable
    {
        private dbEntities context;

        public DirectoryRepository(dbEntities context)
        {
            this.context = context;
        }

        public IEnumerable<Directory> GetDirectories()
        {
            return context.Directory.ToList();
        }

        public Directory GetDirectory(int id)
        {
            return context.Directory.Include(x => x.Directories).SingleOrDefault(x => x.Id == id);
        }

        public void InsertDirectory(Directory directory)
        {
            context.Directory.Add(directory);
        }

        public void DeleteDirectory(int directoryId)
        {
            var directory = context.Directory.Find(directoryId);
            context.Directory.Remove(directory);
        }

        public void DeleteDirectory(Directory directory)
        {
            context.Directory.Remove(directory);
        }

        public void UpdateDirectory(Directory directory)
        {
            context.Entry(directory).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}