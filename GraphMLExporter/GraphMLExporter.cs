using DependencyGraphGeneratorFramework;
using System;

namespace GraphMLExporter
{
    public class GraphMLExporter : DGBaseExporter
    {
        public override string GetHeader()
        {
            /*
                file.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                file.WriteLine("<graphml xmlns=\"http://graphml.graphdrawing.org/xmlns\"");
                file.WriteLine("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                file.WriteLine("xsi:schemaLocation=\"http://graphml.graphdrawing.org/xmlns");
                file.WriteLine("http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd\" > ");
            */
            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<graphml xmlns=\"http://graphml.graphdrawing.org/xmlns\"\nxmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\nxsi:schemaLocation=\"http://graphml.graphdrawing.org/xmlns\nhttp://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd\" > ";
        }

        public override string GetNode(FileInfo fileInfo)
        {
            return String.Format("<key id=\"d0\" for=\"node\" attr.name=\"label\" attr.type=\"string\">\n<default>yellow</default>\n</key>\n<node id=\"{0}\">\n<data key=\"d0\">{0}</data>\n</node>", fileInfo.FileName);
        }

        public override string GetEdge(FileInfo fileInfoFrom, FileInfo fileInfoTo)
        {
            return String.Format("<edge source=\"{0}\" target=\"{1}\"/>", fileInfoFrom.FileName, fileInfoTo.FileName);
        }

        public override string GetFooter()
        {
            return "</graphml>";
        }

        public override string GetExtension()
        {
            return ".graphml";
        }

        public override string GetName()
        {
            return "GraphML Exporter";
        }
    }
}
