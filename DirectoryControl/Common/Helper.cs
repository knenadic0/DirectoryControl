using DirectoryControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DirectoryControl.Common
{
    public class Helper
    {
        public static IEnumerable<Directory> Collect(IEnumerable<Directory> directories)
        {
            foreach (var folder in directories)
            {
                foreach (var child in Collect(folder.Directories.ToList()))
                {
                    yield return child;
                }

                yield return folder;
            }
        }

        public static IEnumerable<string> CollectNames(IEnumerable<Directory> directories, int level = 0)
        {
            foreach (var folder in directories)
            {
                yield return string.Concat(Enumerable.Repeat(" ", level * 5)) + " ┕" + 
                    string.Concat(Enumerable.Repeat("━", 4)) + folder.Name;

                foreach (var child in CollectNames(folder.Directories.ToList(), level + 1))
                {
                    yield return child;
                }
            }
        }
    }
}