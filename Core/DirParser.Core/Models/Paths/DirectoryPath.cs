using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Core.Models.Paths {
    public class DirectoryPath : IPath {
        public string Path { get; private set; }

        public DirectoryPath(string path) {
            this.Path = path;
        }
    }
}
