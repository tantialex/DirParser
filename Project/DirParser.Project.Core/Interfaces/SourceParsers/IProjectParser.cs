using DirParser.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Project.Core {
    public interface IProjectParser {
        ProjectParseReport Parse(DirFile source);
    }
}
