using System.Collections.Generic;

namespace DependencyGraphGeneratorFramework
{
    public class DGFileImport
    {
        public DGFileImport(string fileName)
        {
            this.File = new FileInfo(fileName);
        }
        public FileInfo File { get; set; }
        public List<string> Imports { get; } = new List<string>();
    }
}
