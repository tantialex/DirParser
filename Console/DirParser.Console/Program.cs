using DirParser.Core;
using DirParser.Database.Core;
using DirParser.Informix;
using DirParser.NET;
using DirParser.Procedure.Core;
using DirParser.Project.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DirParser {
    class Program {
        public static void Main(string[] args) {
            string root = args[0];

            IFileReader fileReader = new FileReader();
            IDirectoryBrowser directoryBrowser = new DirectoryBrowser(root, fileReader, new string[] { "node_modules", "$tf" });

            IProjectParser projectParser = new NETCoreProjectParser();
            IDatabaseParser databaseParser = new EntityFrameworkDatabaseParser();
            IProcedureParser procedureParser = new InformixProcedureParser();

            DirFile file;

            List<ProcedureParseReport> list = new List<ProcedureParseReport>();
            while ((file = directoryBrowser.NextFile()) != null) {
                ProjectParseReport projectParseReport = projectParser.Parse(file);

                if (projectParseReport != null && projectParseReport.Name != null) {
                    Console.WriteLine(file.Name);
                    Console.WriteLine(OutputProjectReport(projectParseReport));
                }

                DatabaseParseReport parseReports = databaseParser.Parse(file);

                if (parseReports != null && parseReports.Tables.Count() > 0) {
                    Console.WriteLine(file.Name);
                    Console.WriteLine(OutputDatabaseReport(parseReports));
                }

                ProcedureParseReport procParseReport = procedureParser.Parse(file);

                if (procParseReport != null && procParseReport.Name != null) {
                    list.Add(procParseReport);
                    Console.WriteLine(file.Name);
                    Console.WriteLine(OutputProcedureReport(procParseReport));
                }

                //Console.Write("\rFiles Browsed: {0}, Hits: {1}, Misses: {2}, Current: {3}", ++i, hits, i - hits, file.Name);
            }

            JsonFileWriter.WriteToFile(ParseProcs(list), "output.json");
        }

        private static JObject ParseProcs(IEnumerable<ProcedureParseReport> reports) { 
            JArray nodes = new JArray(reports.Select(x => x.Name).Concat(reports.SelectMany(x => x.Procedures)).Distinct().Select(x => JObject.Parse("{ \"name\":\"" + x + "\", \"group\":1 }")));
            JArray links = new JArray(reports.SelectMany(x => x.Procedures.Select(y => JObject.Parse("{ \"source\":\"" + x.Name + "\", \"target\":\"" + y + "\", \"value\":1 }"))));

            JObject res = new JObject();
            res["nodes"] = nodes;
            res["links"] = links;

            return res;
        }

        private static void RefreshConsole() {
            Console.WriteLine("\r" + new string(' ', Console.WindowWidth - 2));
        }

        private static string OutputProjectReport(ProjectParseReport projectParseReport) {
            string header = "\tProject Report";
            string refs = String.Join(',', projectParseReport.ReferencedProjects.Select(x => x.Name+"."+x.Extension));
            string name = projectParseReport.Name;

            string output = header + "\n" +
                            "\t\tName: " + name + "\n" +
                            "\t\tReferences: " + refs;

            return output;
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
