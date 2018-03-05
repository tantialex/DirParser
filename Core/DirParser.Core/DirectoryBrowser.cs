using DirParser.Core.Models.Paths;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DirParser.Core.Extensions;
using System.Linq;
using System.Collections;

namespace DirParser.Core {
    public class DirectoryBrowser : IDirectoryBrowser {
        private IFileReader _fileReader;
        private Stack<IPath> _stackedPaths;
        private IEnumerable<string> _excludes;

        public DirectoryBrowser(string rootFolder, IFileReader fileReader, IEnumerable<string> excludes = null) {
            if (!Directory.Exists(rootFolder)) {
                throw new ArgumentException("Root folder '" + rootFolder + "' does not exist!");
            }

            this._fileReader = fileReader;
            this._stackedPaths = new Stack<IPath>();
            this._excludes = excludes ?? new string[0];

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
                if (IsExcluded(path)) {
                    Console.WriteLine("Excluded: " + path.Name);
                    continue;
                }
                if (path.GetType() == typeof(FilePath)) {
                    return GenerateFile((FilePath)path);
                } else if(path.GetType() == typeof(DirectoryPath)) {
                    ProcessNextPaths(path.Path);
                }
            }

            return null;
        }

        public bool IsExcluded(IPath path) {
            return _excludes.Contains(path.Name);
        }

        private DirFile GenerateFile(FilePath path) {
            string extension = path.Name.Split('.').Last().ToLower();
            string fileName = path.Name;
            string content = _fileReader.Read(path.Path);

            return new DirFile(fileName, extension, content);
        }
    }
}
