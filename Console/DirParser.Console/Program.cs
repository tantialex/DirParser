using DirParser.Core;
using DirParser.Database.Core;
using DirParser.File.Core;
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

            IDatabaseParser efDatabaseParser = new EntityFrameworkDatabaseParser();
            IProcedureParser procedureParser = new InformixProcedureParser();

            IFileParser csFileParser = new CSFileParser(efDatabaseParser);
            IFileParser sqlFileParser = new SQLFileParser(procedureParser);

            IProjectParser netCoreProjectParser = new NETCoreProjectParser(csFileParser);

            List<ProcedureParseReport> procedureList = new List<ProcedureParseReport>();
            List<ProjectParseReport> projectList = new List<ProjectParseReport>();

            int total = 0;

            DirFile file;
            while ((file = directoryBrowser.NextFile()) != null) {
                if(file.Extension == "csproj") {
                    ProjectParseReport projectReport = netCoreProjectParser.Parse(file);

                    if (projectReport != null) {
                        projectList.Add(projectReport);
                    }
                } else if (file.Extension == "sql") {
                    FileParseReport procedureReport = sqlFileParser.Parse(file);

                    if(procedureReport != null) {
                        procedureList.AddRange(procedureReport.Procedures);
                    }
                }

                total++;
                UpdateConsoleStatus(total, file.Path, root);
            }

            JsonFileWriter.WriteToFile(procedureList, "procedureReport.json");
            JsonFileWriter.WriteToFile(projectList, "projectReport.json");
        }

        private static void UpdateConsoleStatus(int total, string currentPath, string rootPath="") {
            string printPath = currentPath.Substring(rootPath.Length, currentPath.Length - rootPath.Length);
            ClearConsoleLines(0,1);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Files Browsed: {0}", total);
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
