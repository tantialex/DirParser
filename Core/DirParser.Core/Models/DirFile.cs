using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Core {
    public class DirFile {
        public string Name { get; private set; }
        public string Content { get; private set; }

        public DirFile(string name, string content) {
            this.Name = name;
            this.Content = content;
        }
    }
}
