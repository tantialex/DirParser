using DirParser.File.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Project.Core {
    public class ProjectParseReport {
        public string Name { get; private set; }
        public IEnumerable<ReferenceParseReport> ReferencedProjects { get; private set; }
        public IEnumerable<FileParseReport> Files { get; private set; }

        public ProjectParseReport(string name, IEnumerable<ReferenceParseReport> referenceProjects, IEnumerable<FileParseReport> files) {
            this.Name = name;
            this.ReferencedProjects = referenceProjects ?? new ReferenceParseReport[0];
            this.Files = files ?? new FileParseReport[0];
        }
    }
}
