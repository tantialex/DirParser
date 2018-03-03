using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Database.Core {
    public class DatabaseTableReport {
        public string Name { get; private set; }

        public DatabaseTableReport(string name) {
            this.Name = name;
        }
    }
}
