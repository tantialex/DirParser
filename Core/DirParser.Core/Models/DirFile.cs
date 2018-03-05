using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Core {
    public class DirFile {
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public string Content { get; private set; }
        public string Path { get; private set; }

        public DirFile(string name, string extension, string content, string path) {
            this.Name = name;
            this.Extension = extension;
            this.Content = content;
            this.Path = path;
        }
    }
}
