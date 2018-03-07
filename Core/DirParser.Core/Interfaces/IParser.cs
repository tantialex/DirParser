using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Core {
    public interface IParser {
        TReport Parse<TReport>(DirFile file);
    }
}
