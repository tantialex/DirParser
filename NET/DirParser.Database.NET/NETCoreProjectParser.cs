using DirParser.Core;
using DirParser.Project.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DirParser.NET {
    public class NETCoreProjectParser : IProjectParser {
        private static readonly Regex NAME_REGEX = new Regex("(?<name>.+)\\.(?<ext>.+)");
        private static readonly Regex REFERENCE_REGEX = new Regex("<ProjectReference Include=\"[.]*\\\\(?:.+\\\\)*(?<refName>.+)\\.(?<ext>.+)\"");

        public ProjectParseReport Parse(DirFile file) {
            ProjectParseReport databaseReport = null;

            if(file.Extension == "csproj") {
                databaseReport = new ProjectParseReport(GetName(file.Name), GetReferences(file.Content));
            }

            return databaseReport;
        }

        private string GetName(string source) {
            MatchCollection matches = NAME_REGEX.Matches(source);

            Match match = matches.FirstOrDefault();
                
            return match.Groups["name"].Value.Trim();
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
