using DirParser.Core;
using DirParser.Database.Core;
using DirParser.Database.NET;
using DirParser.Procedure.Core;
using DirParser.Procedure.Informix;
using System;
using System.Linq;

namespace DirParser {
    class Program {
        public static void Main(string[] args) {
            string root = args[0];

            IFileReader fileReader = new FileReader();
            IDirectoryBrowser directoryBrowser = new DirectoryBrowser(root, fileReader);

            IDatabaseParser databaseParser = new EntityFrameworkDatabaseParser();
            IProcedureParser procedureParser = new InformixProcedureParser();


            DirFile file;
            int i = 0;
            int hits = 0;
            while ((file = directoryBrowser.NextFile()) != null) {
                DatabaseParseReport parseReports = databaseParser.Parse(file.Content);

                if (parseReports.Tables.Count() > 0) {
                    RefreshConsole();
                    Console.WriteLine(file.Name);
                    Console.WriteLine(OutputDatabaseReport(parseReports));
                    hits++;
                }

                ProcedureParseReport procParseReport = procedureParser.Parse(file.Content);

                if (procParseReport.Name != null) {
                    RefreshConsole();
                    Console.WriteLine(file.Name);
                    Console.WriteLine(OutputProcedureReport(procParseReport));
                }

                Console.Write("\rFiles Browsed: {0}, Hits: {1}, Misses: {2}", ++i, hits, i - hits);
            }
        }

        private static void RefreshConsole() {
            Console.WriteLine("\r" + new string(' ', Console.WindowWidth - 2));
        }

        private static string OutputDatabaseReport(DatabaseParseReport dbReport) {
            string header = "\tDatabase Report";
            string tables = String.Join(',', dbReport.Tables.Select(x => x.Name));

            string output = header + "\n" +
                            "\t\tTables: " + tables;

            return output;
        }

        private static string OutputProcedureReport(ProcedureParseReport dbReport) {
            string header = "\tProcedure Report";
            string tables = String.Join(',', dbReport.Tables.Select(x => x.Name));
            string procs = String.Join(',', dbReport.Procedures);

            string output = header + "\n" +
                            "\t\tTables: " + tables + "\n" +
                            "\t\tProcedures: " + procs;

            return output;
        }
    }
}
