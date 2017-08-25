using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace safemooneyBackend.Infrastructure
{
    public class FileSystemModule
    {
        public String ActiveDirectory { get; private set; }
        
        public FileSystemModule(String activeDirectory)
        {
            ActiveDirectory = activeDirectory;
        }
    }
}