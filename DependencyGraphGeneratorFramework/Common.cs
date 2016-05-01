using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace DependencyGraphGeneratorFramework
{
    public class Common
    {
        public delegate int DelFileAnalyzers(ref FileInfo fi);

        public static FileInfo[] CreateDependencyGraph(string[] FileNames, string[] VendorsToExclude, DelFileAnalyzers FileAnalyzers)
        {
            List<FileInfo> FileInfosReturn = new List<FileInfo>();
            if (FileAnalyzers != null)
            {
                List<FileInfo> FileInfosFullList = new List<FileInfo>();
                foreach (string fn in FileNames)
                {
                    FileInfo fi = new FileInfo(Path.GetFileName(fn));
                    fi.FileNameFullPath = fn;
                    AnalyzeFile(ref fi, ref FileInfosFullList, VendorsToExclude, FileAnalyzers);
                    FileInfosReturn.Add(fi);
                }
            }
            return FileInfosReturn.ToArray();
        }

        public static DelFileAnalyzers LoadFileAnalyzers()
        {
            DelFileAnalyzers del = null;
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] files = Directory.GetFiles(path, "*.dll");

            if(files!=null)
            {
                foreach (string dll in files)
                {
                    Assembly a = Assembly.LoadFile(dll);
                    foreach (Type t in a.GetTypes())
                    {
                        if(t.GetInterface("IDGFileAnalyzer") != null)
                        {
                            try
                            {
                                IDGFileAnalyzer fa = Activator.CreateInstance(t) as IDGFileAnalyzer;
                                del += fa.AnalyzeFile;
                            }
                            catch
                            {
                                // nothing to do here :(
                            }
                        }
                    }
                }
            }

            return del;
        }

        public static IDGGraphExporter[] GetExporters()
        {
            List<IDGGraphExporter> exporters = new List<IDGGraphExporter>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] files = Directory.GetFiles(path, "*.dll");

            if (files != null)
            {
                foreach (string dll in files)
                {
                    Assembly a = Assembly.LoadFile(dll);
                    foreach (Type t in a.GetTypes())
                    {
                        if (t.GetInterface("IDGGraphExporter") != null)
                        {
                            try
                            {
                                IDGGraphExporter ge = Activator.CreateInstance(t) as IDGGraphExporter;
                                exporters.Add(ge);
                            }
                            catch
                            {
                                // nothing to do here :(
                            }
                        }
                    }
                }
            }

            return exporters.ToArray();
        }

        private static void AddVendorInformation(ref FileInfo file)
        {
            // try to get vendor information
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(file.FileNameFullPath);
            file.Properties.Add("Company", myFileVersionInfo.CompanyName);
            file.Properties.Add("LegalCopyright", myFileVersionInfo.LegalCopyright);

            X509Certificate2 theCertificate;

            try
            {
                X509Certificate theSigner = X509Certificate.CreateFromSignedFile(file.FileNameFullPath);
                theCertificate = new X509Certificate2(theSigner);
                file.Properties.Add("Certificate", "Yes"/*theCertificate.ToString()*/);
            }
            catch
            {
                // no certificate
                file.Properties.Add("Certificate", "No");
            }
        }

        private static bool ShouldFileBeExcluded(FileInfo file, string[] VendorsToExclude)
        {
            foreach (string vendor in VendorsToExclude)
            {
                string s = "";
                if (file.Properties.TryGetValue("LegalCopyright", out s))
                {
                    if (s != null && s.ToLower().Contains(vendor.ToLower()))
                    {
                        return true;
                    }
                }
                if (file.Properties.TryGetValue("Company", out s))
                {
                    if (s != null && s.ToLower().Contains(vendor.ToLower()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void AnalyzeFile(ref FileInfo file, ref List<FileInfo> FileInfosFullList, string[] VendorsToExclude, DelFileAnalyzers FileAnalyzers)
        {
            if(FileInfosFullList.Contains(file))
            {
                file = FileInfosFullList.ElementAt(FileInfosFullList.IndexOf(file));
            }
            else
            {
                AddVendorInformation(ref file);

                FileAnalyzers(ref file);
                FileInfosFullList.Add(file);

                string SystemDir = Environment.SystemDirectory;
                string WinDir = Environment.GetEnvironmentVariable("windir");
                string[] EnvPaths = Environment.GetEnvironmentVariable("path").Split(new char[] { ';' });

                List<DGFileImport> CleanImports = new List<DGFileImport>();
                foreach(DGFileImport import in file.Imports)
                {
                    string path = Path.GetDirectoryName(file.FileNameFullPath);

                    if(File.Exists(path+"\\"+import.File.FileName))
                    {
                        import.File.FileNameFullPath = path + "\\" + import.File.FileName;
                    }
                    else if(File.Exists(SystemDir + "\\"+import.File.FileName))
                    {
                        import.File.FileNameFullPath = SystemDir + "\\" + import.File.FileName;
                    }
                    else if(File.Exists(WinDir + "\\" + import.File.FileName))
                    {
                        import.File.FileNameFullPath = WinDir + "\\" + import.File.FileName;
                    }
                    else
                    {
                        foreach(string p in EnvPaths)
                        {
                            if(File.Exists(p + "\\" + import.File.FileName))
                            {
                                import.File.FileNameFullPath = p + "\\" + import.File.FileName;
                                break;
                            }
                        }
                        if(import.File.FileNameFullPath==null)
                        {
                            continue;
                        }
                    }

                    FileInfo fi = import.File;
                    if (!ShouldFileBeExcluded(fi, VendorsToExclude))
                    {
                        AnalyzeFile(ref fi, ref FileInfosFullList, VendorsToExclude, FileAnalyzers);
                    }
                    import.File = fi;
                }

                foreach (DGFileImport i in file.Imports)
                {
                    if (!ShouldFileBeExcluded(i.File, VendorsToExclude))
                    {
                        CleanImports.Add(i);
                    }
                }
                file.Imports.Clear();
                file.Imports.AddRange(CleanImports);
            }
        }
    }
}
