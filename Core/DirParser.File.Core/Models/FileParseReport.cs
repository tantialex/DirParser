using DirParser.Database.Core;
using DirParser.Procedure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.File.Core {
    public class FileParseReport {
        public string Name { get; private set; }
        public IEnumerable<DatabaseParseReport> Databases { get; private set; }
        public IEnumerable<ProcedureParseReport> Procedures { get; private set; }

        public FileParseReport(string name, IEnumerable<DatabaseParseReport> databases, IEnumerable<ProcedureParseReport> procedures) {
            this.Name = name;
            this.Databases = databases ?? new DatabaseParseReport[0];
            this.Procedures = procedures ?? new ProcedureParseReport[0];
        }
    }
}
