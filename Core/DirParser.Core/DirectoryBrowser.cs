using DirParser.Core.Models.Paths;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DirParser.Core.Extensions;

namespace DirParser.Core {
    public class DirectoryBrowser : IDirectoryBrowser {
        private IFileReader _fileReader;
        private Stack<IPath> _stackedPaths;

        public DirectoryBrowser(string rootFolder, IFileReader fileReader) {
            if (!Directory.Exists(rootFolder)) {
                throw new ArgumentException("Root folder '" + rootFolder + "' does not exist!");
            }

            this._fileReader = fileReader;
            this._stackedPaths = new Stack<IPath>();

            Init(rootFolder);
        }

        private void Init(string rootFolder) {
            ProcessNextPaths(rootFolder);
        }

        private void ProcessNextPaths(string source) {
            string[] dirs = Directory.GetDirectories(source);
            dirs.ForEachReverse(x => _stackedPaths.Push(new DirectoryPath(x)));

            string[] files = Directory.GetFiles(source);
            files.ForEachReverse(x => _stackedPaths.Push(new FilePath(x)));
        }

        public DirFile NextFile() {
            IPath path;

            while(_stackedPaths.TryPop(out path)) {
                if (path.GetType() == typeof(FilePath)) {
                    return GenerateFile((FilePath)path);
                } else if(path.GetType() == typeof(DirectoryPath)) {
                    ProcessNextPaths(path.Path);
                }
            }

            return null;
        }

        private DirFile GenerateFile(FilePath path) {
            string fileName = Path.GetFileName(path.Path);
            string content = _fileReader.Read(path.Path);

            return new DirFile(fileName, content);
        }
    }
}
