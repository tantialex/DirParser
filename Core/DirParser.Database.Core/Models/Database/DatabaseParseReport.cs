using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Database.Core {
    public class DatabaseParseReport {
        public IEnumerable<DatabaseTableReport> Tables { get; private set; }

        public DatabaseParseReport(IEnumerable<DatabaseTableReport> tables) {
            this.Tables = tables ?? new DatabaseTableReport[0];
        }
    }
}
