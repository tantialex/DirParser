using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirParser.Core {
    public class FileReader : IFileReader {
        public string Read(string path) {
            return File.ReadAllText(path);
        }
    }
}
