using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Core.Models.Paths {
    public class DirectoryPath : IPath {
        public string Path { get; private set; }
        public string Name { get; private set; }

        public DirectoryPath(string path) {
            int preDir = System.IO.Path.GetDirectoryName(path).Length + 1;
            this.Name = path.Substring(preDir, path.Length - preDir);
            this.Path = path;
        }
    }
}
