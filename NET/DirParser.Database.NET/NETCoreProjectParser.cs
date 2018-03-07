using DirParser.Core;
using DirParser.Database.Core;
using DirParser.File.Core;
using DirParser.Procedure.Core;
using DirParser.Project.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DirParser.NET {
    public class NETCoreProjectParser : IProjectParser {
        private static readonly Regex REFERENCE_REGEX = new Regex("<ProjectReference Include=\"[.]*\\\\(?:.+\\\\)*(?<refName>.+)\\.(?<ext>.+)\"");

        private IEnumerable<IFileParser> _fileParsers;

        public NETCoreProjectParser(params IFileParser[] fileParsers) {
            this._fileParsers = fileParsers ?? new IFileParser[0];
        }

        public ProjectParseReport Parse(DirFile file) {
            List<FileParseReport> fileReports = new List<FileParseReport>();

            //TODO: Inject
            string root = file.Path.Substring(0, file.Path.Length - file.Name.Length);
            DirectoryBrowser directoryBrowser = new DirectoryBrowser(root, new FileReader(), new string[] { "node_modules" });

            DirFile currFile;
            while ((currFile = directoryBrowser.NextFile()) != null) {
                List<FileParseReport> currFileReports = new List<FileParseReport>();

                foreach (IFileParser parser in _fileParsers) {
                    currFileReports.Add(parser.Parse(currFile));
                }

                fileReports.Add(new FileParseReport(currFile.Name, currFileReports.Where(x => x != null).SelectMany(x => x.Databases), currFileReports.Where(x => x != null).SelectMany(x => x.Procedures)));
            }

            return new ProjectParseReport(file.Name, GetReferences(file.Content), fileReports);
        }

        private IEnumerable<ReferenceParseReport> GetReferences(string source) {
            MatchCollection matches = REFERENCE_REGEX.Matches(source);

            IEnumerable<ReferenceParseReport> refs = matches.Select(x =>
                new ReferenceParseReport(
                    x.Groups["refName"].Value.Trim(),
                    x.Groups["ext"].Value.Trim()
                )
            );

            return refs;
        }
    }
}
