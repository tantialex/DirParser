using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirParser.Core {
    public interface IFileReader {

        string Read(string input);
    }
}
