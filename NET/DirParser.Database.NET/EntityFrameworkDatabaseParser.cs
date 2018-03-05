using DirParser.Core;
using DirParser.Database.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DirParser.NET {
    public class EntityFrameworkDatabaseParser : IDatabaseParser {
        private static readonly Regex TABLE_NAME_REGEX = new Regex("ToTable\\(\"(?<tableName>[^ \"]*)\"");

        public DatabaseParseReport Parse(DirFile file) {
            DatabaseParseReport databaseReport = null;

            if (file.Extension == "cs") {
                databaseReport = new DatabaseParseReport(GetTables(file.Content));
            }

            return databaseReport;
        }

        private IEnumerable<DatabaseTableReport> GetTables(string source) {
            MatchCollection matches = TABLE_NAME_REGEX.Matches(source);

            IEnumerable<DatabaseTableReport> tables = matches.Select(x => 
                new DatabaseTableReport( 
                    x.Groups["tableName"].Value
                )
            );

            return tables;
        }
    }
}
