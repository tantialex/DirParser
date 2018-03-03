using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Database.Core {
    public interface IDatabaseParser {
        DatabaseParseReport Parse(string source);
    }
}
