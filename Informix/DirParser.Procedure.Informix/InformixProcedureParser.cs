﻿using DirParser.Procedure.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DirParser.Procedure.Informix {
    public class InformixProcedureParser : IProcedureParser {
        private static readonly Regex NAME_REGEX = new Regex("CREATE PROCEDURE[ ]+\"informix\"(?<procName>[^ ,(]*)");
        private static readonly Regex PROCEDURE_REGEX = new Regex("execute procedure[ ]+(?<procName>[^ ,(]+)");
        private static readonly Regex TABLE_REGEX = new Regex("[\\n,\\r, ,\\t]from[ ]+(?<tableName>[^ ,\\n,;]+)"); //TODO: Fix to accept after comma

        public ProcedureParseReport Parse(string source) {
            ProcedureParseReport databaseReport = null;

            databaseReport = new ProcedureParseReport(GetName(source), GetProcedures(source), GetTables(source));

            return databaseReport;
        }

        private string GetName(string source) {
            MatchCollection matches = NAME_REGEX.Matches(source);

            Match match = matches.FirstOrDefault();
            
            if(match != null) {
                return match.Groups["procName"].Value.Trim();
            }
        
            return null;
        }

        private IEnumerable<ProcedureTableReport> GetTables(string source) {
            MatchCollection matches = TABLE_REGEX.Matches(source);

            IEnumerable<ProcedureTableReport> tables = matches.Select(x => 
                new ProcedureTableReport( 
                    x.Groups["tableName"].Value.Trim()
                )
            );

            return tables.Distinct();
        }

        private IEnumerable<string> GetProcedures(string source) {
            MatchCollection matches = PROCEDURE_REGEX.Matches(source);

            IEnumerable<string> procs = matches.Select(x =>
                    x.Groups["procName"].Value.Trim()
            );

            return procs.Distinct();
        }
    }
}
