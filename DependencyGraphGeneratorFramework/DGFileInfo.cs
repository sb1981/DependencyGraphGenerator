using System;
using System.Collections.Generic;

namespace DependencyGraphGeneratorFramework
{
    public class FileInfo : IEquatable<FileInfo>
    {
        public FileInfo(string fileName)
        {
            this.FileName = fileName;
        }
        public string FileName { get; }
        public string FileNameFullPath { get; set; }
        public Dictionary<string, string> Properties { get; } = new Dictionary<string, string>();
        public List<string> Exports { get; } = new List<string>();
        public List<DGFileImport> Imports { get; } = new List<DGFileImport>();

        public bool Equals(FileInfo other)
        {
            if (other.FileNameFullPath == this.FileNameFullPath)
            {
                return true;
            }
            return false;
        }
    }
}
