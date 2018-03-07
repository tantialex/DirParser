using DirParser.Core;
using DirParser.File.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Procedure.Core {
    public interface IFileParser {
        FileParseReport Parse(DirFile file);
    }
}
