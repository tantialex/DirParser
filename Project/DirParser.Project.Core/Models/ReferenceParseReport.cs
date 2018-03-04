using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Project.Core {
    public class ReferenceParseReport {
        public string Name { get; private set; }
        public string Extension { get; private set; }

        public ReferenceParseReport(string name, string extension) {
            this.Name = name;
            this.Extension = extension;
        }
    }
}
