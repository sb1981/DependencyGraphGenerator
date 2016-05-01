using DependencyGraphGeneratorFramework;
using System;
using System.IO;

namespace GraphvizExporter
{
    public class GraphvizExporter : DGBaseExporter
    {
        public override string GetHeader()
        {
            return "digraph DGGgraph\n{\nnode [style=filled];";
        }

        public override string GetNode(DependencyGraphGeneratorFramework.FileInfo fileInfo)
        {
            string color = "black";
            switch (Path.GetExtension(fileInfo.FileName).ToLower())
            {
                case ".dll":
                    color = "LightBlue";
                    break;
                case ".exe":
                    color = "PaleGreen";
                    break;
            }

            return String.Format("\"{0}\" [fillcolor={1}];", fileInfo.FileName, color);
        }

        public override string GetEdge(DependencyGraphGeneratorFramework.FileInfo fileInfoFrom, DependencyGraphGeneratorFramework.FileInfo fileInfoTo)
        {
            return String.Format("\"{0}\" -> \"{1}\";", fileInfoFrom.FileName, fileInfoTo.FileName);
        }

        public override string GetFooter()
        {
            return "}";
        }

        public override string GetName()
        {
            return "Graphviz Exporter";
        }

        public override string GetExtension()
        {
            return ".dot";
        }
    }
}
