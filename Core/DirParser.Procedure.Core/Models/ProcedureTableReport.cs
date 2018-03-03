using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Procedure.Core {
    public class ProcedureTableReport {
        public string Name { get; private set; }

        public ProcedureTableReport(string name) {
            this.Name = name;
        }
    }
}
