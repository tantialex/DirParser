using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirParser {
    public static class JsonFileWriter {

        public static void WriteToFile<TObject>(TObject obj, string path) {
            string content = JsonConvert.SerializeObject(obj);

            System.IO.File.WriteAllText(path, content);
        }
    }
}
