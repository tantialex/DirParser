using System;
using System.Collections.Generic;
using System.Text;

namespace DirParser.Core.Models.Paths {
    public interface IPath {
        string Path { get; }
        string Name { get; }
    }
}
