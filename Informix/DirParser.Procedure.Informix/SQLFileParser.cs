using DirParser.Core;
using DirParser.File.Core;
using DirParser.Procedure.Core;
using System;
using System.Collections.Generic;

namespace DirParser.Informix {
    public class SQLFileParser : IFileParser {
        private IEnumerable<IProcedureParser> _procedureParsers;
        public SQLFileParser(params IProcedureParser[] procedureParsers) {
            this._procedureParsers = procedureParsers;
        }

        public FileParseReport Parse(DirFile file) {
            FileParseReport databaseReport = null;

            if (file.Extension == "sql") {
                List<ProcedureParseReport> procedureParseReports = new List<ProcedureParseReport>();

                foreach (IProcedureParser procParser in _procedureParsers) {
                    ProcedureParseReport procReport = procParser.Parse(file);

                    if (procReport != null) {
                        procedureParseReports.Add(procReport);
                    }
                }

                databaseReport = new FileParseReport(file.Name, null, procedureParseReports);
            }

            return databaseReport;
        }
    }
}
