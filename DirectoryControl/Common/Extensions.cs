using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DirectoryControl.Common
{
    public static class Extensions
    {
        public static int? ToValueOrNull(this int? id)
        {
            return id != 0 ? id : null;
        }
    }
}