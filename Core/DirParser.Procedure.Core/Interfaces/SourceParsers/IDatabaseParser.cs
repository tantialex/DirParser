using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Procedure.Core {
    public interface IProcedureParser {
        ProcedureParseReport Parse(string source);
    }
}
