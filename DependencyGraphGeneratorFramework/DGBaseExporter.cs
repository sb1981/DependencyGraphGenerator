using System.Collections.Generic;

namespace DependencyGraphGeneratorFramework
{
    public abstract class DGBaseExporter : IDGGraphExporter
    {
        public int ExportGraph(FileInfo[] fileInfos, string fileName)
        {
            if(fileInfos == null)
            {
                return 1;
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                string header = GetHeader();
                if(header != null)
                {
                    file.WriteLine(header);
                }

                List<string> nodes = new List<string>();
                List<FileInfo> prevNodes = new List<FileInfo>();
                foreach (FileInfo fileInfo in fileInfos)
                {
                    ExportNode(file, fileInfo, nodes, prevNodes);
                }
                foreach (string s in nodes)
                {
                    file.WriteLine(s);
                }


                List<string> edges = new List<string>();
                prevNodes = new List<FileInfo>();
                foreach (FileInfo fileInfo in fileInfos)
                {
                    ExportEdge(file, fileInfo, edges, prevNodes);
                }
                foreach (string s in edges)
                {
                    file.WriteLine(s);
                }


                string footer = GetFooter();
                if (footer != null)
                {
                    file.WriteLine(footer);
                }
            }
            return 0;
        }

        public void ExportNode(System.IO.StreamWriter file, FileInfo fileInfo, List<string> nodes, List<FileInfo> prevNodes)
        {
            if (prevNodes.Contains(fileInfo))
            {
                return;
            }

            string node = GetNode(fileInfo);
            if(node != null && !nodes.Contains(node))
            {
                nodes.Add(node);
            }

            foreach (DGFileImport import in fileInfo.Imports)
            {
                List<FileInfo> NewPrevNodes = new List<FileInfo>(prevNodes);
                NewPrevNodes.Add(fileInfo);
                ExportNode(file, import.File, nodes, NewPrevNodes);
            }
        }

        public void ExportEdge(System.IO.StreamWriter file, FileInfo fileInfo, List<string> edges, List<FileInfo> prevNodes)
        {
            if (prevNodes.Contains(fileInfo))
            {
                return;
            }
            foreach (DGFileImport import in fileInfo.Imports)
            {
                string s = GetEdge(fileInfo, import.File);

                if (s != null && !edges.Contains(s))
                {
                    edges.Add(s);
                }

                List<FileInfo> NewPrevNodes = new List<FileInfo>(prevNodes);
                NewPrevNodes.Add(fileInfo);
                ExportEdge(file, import.File, edges, NewPrevNodes);
            }
        }

        abstract public string GetHeader();
        abstract public string GetNode(FileInfo fileInfo);
        abstract public string GetEdge(FileInfo fileInfoFrom, FileInfo fileInfoTo);
        abstract public string GetFooter();
        public abstract string GetName();
        public abstract string GetExtension();
    }
}
