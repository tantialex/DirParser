using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirParser.Core.Models.Paths {
    public class FilePath : IPath {
        public string Path { get; private set; }
        public string Name { get; private set; }

        public FilePath(string path) {
            this.Path = path;
            this.Name = System.IO.Path.GetFileName(path);
        }
    }
}
