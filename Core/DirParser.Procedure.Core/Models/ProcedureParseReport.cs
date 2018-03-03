using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Procedure.Core {
    public class ProcedureParseReport {
        public string Name { get; private set; }
        public IEnumerable<string> Procedures { get; private set; }
        public IEnumerable<ProcedureTableReport> Tables { get; private set; }

        public ProcedureParseReport(string name, IEnumerable<string> procedures, IEnumerable<ProcedureTableReport> tables) {
            this.Name = name;
            this.Procedures = procedures ?? new string[0];
            this.Tables = tables ?? new ProcedureTableReport[0];
        }
    }
}
