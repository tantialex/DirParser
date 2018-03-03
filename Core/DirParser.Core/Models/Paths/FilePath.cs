using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Core.Models.Paths {
    public class FilePath : IPath {
        public string Path { get; private set; }

        public FilePath(string path) {
            this.Path = path;
        }
    }
}
