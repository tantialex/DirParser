using DirParser.Core;
using DirParser.Database.Core;
using DirParser.File.Core;
using DirParser.Procedure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.NET {
    public class CSFileParser : IFileParser {
        private IEnumerable<IDatabaseParser> _databaseParsers;

        public CSFileParser(params IDatabaseParser[] databaseParsers) {
            this._databaseParsers = databaseParsers;
        }

        public FileParseReport Parse(DirFile file) {
            FileParseReport databaseReport = null;

            if (file.Extension == "cs") {
                List<DatabaseParseReport> databaseParseReports = new List<DatabaseParseReport>();

                foreach(IDatabaseParser dbParser in _databaseParsers) {
                    DatabaseParseReport dbReport = dbParser.Parse(file);

                    if(dbReport != null) {
                        databaseParseReports.Add(dbReport);
                    }
                }

                databaseReport = new FileParseReport(file.Name, databaseParseReports, null);
            }

            return databaseReport;
        }
    }
}
