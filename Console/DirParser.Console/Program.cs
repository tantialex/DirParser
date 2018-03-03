using DirParser.Core;
using DirParser.Database.Core;
using DirParser.Database.NET;
using System;
using System.Linq;

namespace DirParser {
    class Program {
        public static void Main(string[] args) {
            string root = args[0];

            IFileReader fileReader = new FileReader();
            IDirectoryBrowser directoryBrowser = new DirectoryBrowser(root, fileReader);

            IDatabaseParser databaseParser = new EntityFrameworkDatabaseParser();

            DirFile file;
            while ((file = directoryBrowser.NextFile()) != null) {
                DatabaseParseReport parseReports = databaseParser.Parse(file.Content);

                if(parseReports.Tables.Count() > 0) {
                    Console.WriteLine(file.Name);
                    Console.WriteLine(OutputDatabaseReport(parseReports));
                }
            }
        }

        private static string OutputDatabaseReport(DatabaseParseReport dbReport) {
            string tables = String.Join(',', dbReport.Tables.Select(x => x.Name));
            string header = "\tDatabase Report";

            string output = header + "\n" +
                            "\t\tTables: " + tables;

            return output;
        }
    }
}
