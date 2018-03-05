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
            IDirectoryBrowser directoryBrowser = new DirectoryBrowser(root, fileReader, new string[] { "node_modules", "$tf", "packages" });

            IProjectParser projectParser = new NETCoreProjectParser();
            IDatabaseParser databaseParser = new EntityFrameworkDatabaseParser();
            IProcedureParser procedureParser = new InformixProcedureParser();

            DirFile file;

            List<ProcedureParseReport> procedureList = new List<ProcedureParseReport>();
            List<DatabaseParseReport> databaseList = new List<DatabaseParseReport>();
            List<ProjectParseReport> projectList = new List<ProjectParseReport>();
            int total = 0;
            int hits = 0;
            while ((file = directoryBrowser.NextFile()) != null) {

                bool tempHit = false;
                ProjectParseReport projectParseReport = projectParser.Parse(file);

                if (projectParseReport != null && projectParseReport.Name != null) {
                    projectList.Add(projectParseReport);
                    tempHit = true;
                }

                DatabaseParseReport parseReports = databaseParser.Parse(file);

                if (parseReports != null && parseReports.Tables.Count() > 0) {
                    databaseList.Add(parseReports);
                    tempHit = true;
                }

                ProcedureParseReport procParseReport = procedureParser.Parse(file);

                if (procParseReport != null && procParseReport.Name != null) {
                    procedureList.Add(procParseReport);
                    tempHit = true;
                }

                total++;
                if (tempHit) {
                    hits++;
                }

                UpdateConsoleStatus(total, hits, total - hits, file.Path, root);
            }

            JsonFileWriter.WriteToFile(procedureList, "procedureReport.json");
            JsonFileWriter.WriteToFile(databaseList, "databaseReport.json");
            JsonFileWriter.WriteToFile(projectList, "projectReport.json");
        }

        private static void UpdateConsoleStatus(int total, int hits, int misses, string currentPath, string rootPath="") {
            string printPath = currentPath.Substring(rootPath.Length, currentPath.Length - rootPath.Length);
            ClearConsoleLines(0,1);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Files Browsed: {0}, Hits: {1}, Misses: {2}", total, hits, misses);
            Console.WriteLine(printPath.Length > Console.WindowWidth ? printPath.Substring(0, Console.WindowWidth) : printPath);
        }
        private static void ClearConsoleLines(params int[] lineNums) {
            int currentLineCursor = Console.CursorTop;
            foreach(int i in lineNums) {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
