using System;
using System.Collections.Generic;
using DependencyGraphGeneratorFramework;

namespace CSVExporter
{
    public class CSVExporter : DGBaseExporter
    {
        public override string GetEdge(FileInfo fileInfoFrom, FileInfo fileInfoTo)
        {
            return "";
        }

        public override string GetExtension()
        {
            return ".csv";
        }

        public override string GetFooter()
        {
            return "";
        }

        public override string GetHeader()
        {
            return "Filename; Full Path; Platform; Company; Legal Copyright; Certificate; ASLR; DEP; NO_SEH";
        }

        public override string GetName()
        {
            return "CSV Exporter";
        }

        public override string GetNode(FileInfo fileInfo)
        {
            string platform = GetProperty("Platform", fileInfo.Properties);
            string company = GetProperty("Company", fileInfo.Properties);
            string legal = GetProperty("LegalCopyright", fileInfo.Properties);
            string cert = GetProperty("Certificate", fileInfo.Properties);
            string aslr = GetProperty("ASLR", fileInfo.Properties);
            string dep = GetProperty("DEP", fileInfo.Properties);
            string no_seh = GetProperty("NO_SEH", fileInfo.Properties);

            return String.Format("{0}; {1}; {2}; {3}; {4}; {5}; {6}; {7}; {8}",
                fileInfo.FileName,
                fileInfo.FileNameFullPath,
                platform,
                company,
                legal,
                cert,
                aslr,
                dep,
                no_seh
                ); 
        }

        private string GetProperty(string key, Dictionary<string, string> properties)
        {
            if (properties.ContainsKey(key))
            {
                return properties[key];
            }
            return "unknown";
        }
    }
}
