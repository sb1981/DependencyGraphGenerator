using DependencyGraphGeneratorFramework;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DependencyGraphGenerator
{
    public partial class FormMainWindow : Form
    {
        private TreeView treeViewMain = new TreeView();
        private Microsoft.Msagl.GraphViewerGdi.GViewer GraphViewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        private Common.DelFileAnalyzers FileAnalyzers;
        private FileInfo[] FileInfos;

        public FormMainWindow()
        {
            InitializeComponent();
            treeViewMain.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(treeViewMain);

            GraphViewer.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(GraphViewer);

            FileAnalyzers = Common.LoadFileAnalyzers();

            if(FileAnalyzers == null)
            {
                MessageBox.Show("No plugins found","Warning");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            List<string> fileNames = new List<string>();
            List<string> vendorsToExclude = new List<string>();
            
            if ((new FormOpenFile()).ShowDialog(fileNames, vendorsToExclude))
            {
                FileInfos = Common.CreateDependencyGraph(
                        fileNames.ToArray(), vendorsToExclude.ToArray(), FileAnalyzers);

                treeViewMain.Nodes.Clear();
                Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("Dependencies");
                foreach(FileInfo fi in FileInfos)
                {
                    List<FileInfo> PrevNodes = new List<FileInfo>();
                    TreeNode treeNode = new TreeNode(fi.FileName);
                    treeViewMain.Nodes.Add(treeNode);
                    AddFileInfoTreeNodes(fi, treeNode, PrevNodes);
                    AddFileInfoGraphNodes(fi, graph, PrevNodes);
                }
                GraphViewer.Graph = graph;
            }
        }

        private void AddFileInfoGraphNodes(FileInfo fileInfo, Microsoft.Msagl.Drawing.Graph graph, List<FileInfo> PrevNodes)
        {
            if(PrevNodes.Contains(fileInfo))
            {
                return;
            }

            if (fileInfo.Imports.Count != 0)
            {
                foreach (DGFileImport import in fileInfo.Imports)
                {
                    Microsoft.Msagl.Drawing.Node n = graph.FindNode(fileInfo.FileName);
                    bool addEdge = true;
                    if(n != null)
                    {
                        foreach(Microsoft.Msagl.Drawing.Edge e in n.OutEdges)
                        {
                            if(e.Target==import.File.FileName)
                            {
                                addEdge = false;
                            }
                        }
                    }
                    if(addEdge)
                    {
                        graph.AddEdge(fileInfo.FileName, import.File.FileName);
                    }
                    List<FileInfo> NewPrevNodes = new List<FileInfo>(PrevNodes);
                    NewPrevNodes.Add(fileInfo);
                    AddFileInfoGraphNodes(import.File, graph, NewPrevNodes);
                }
            }
            else
            {
                graph.AddNode(fileInfo.FileName);
            }
            Microsoft.Msagl.Drawing.Color c = Microsoft.Msagl.Drawing.Color.White;
            switch (System.IO.Path.GetExtension(fileInfo.FileName).ToLower())
            {
                case ".dll":
                    c = Microsoft.Msagl.Drawing.Color.LightBlue;
                    break;
                case ".exe":
                    c = Microsoft.Msagl.Drawing.Color.LightGreen;
                    break;
            }
            graph.FindNode(fileInfo.FileName).Attr.FillColor = c;
        }

        private void AddFileInfoTreeNodes(FileInfo fileInfo, TreeNode root, List<FileInfo> PrevNodes)
        {
            if (PrevNodes.Contains(fileInfo))
            {
                return;
            }

            TreeNode prop = new TreeNode("Properties");
            root.Nodes.Add(prop);

            prop.Nodes.Add("FullPath - " + fileInfo.FileNameFullPath);

            foreach (KeyValuePair<string, string> entry in fileInfo.Properties)
            {
                prop.Nodes.Add(entry.Key + " - " + entry.Value);
            }
            
            if(fileInfo.Exports.Count != 0)
            {
                TreeNode exports = new TreeNode("Exports");
                root.Nodes.Add(exports);
                foreach(string e in fileInfo.Exports)
                {
                    exports.Nodes.Add(e);
                }
            }
            
            if (fileInfo.Imports.Count != 0)
            {
                TreeNode imports = new TreeNode("Imports");
                root.Nodes.Add(imports);
                foreach (DGFileImport import in fileInfo.Imports)
                {
                    TreeNode i = new TreeNode(import.File.FileName);
                    imports.Nodes.Add(i);
                    List<FileInfo> NewPrevNodes = new List<FileInfo>(PrevNodes);
                    NewPrevNodes.Add(fileInfo);
                    AddFileInfoTreeNodes(import.File, i, NewPrevNodes);
                    
                    TreeNode im = new TreeNode(import.File.FileName + " imported");
                    imports.Nodes.Add(im);
                    foreach (string e in import.Imports)
                    {
                        im.Nodes.Add(e);
                    }
                }
            }

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            if(FileInfos == null)
            {
                MessageBox.Show("Nothing to export -> aborting", "Warning");
                return;
            }
            IDGGraphExporter[] exporters = Common.GetExporters();
            if (exporters == null)
            {
                MessageBox.Show("no exporters found -> aborting", "Warning");
                return;
            }

            (new FormExportFile(exporters, FileInfos)).ShowDialog();

            //            DependencyGraphGeneratorFramework.IDGGraphExporter exporter = new GraphMLExporter.GraphMLExporter();
            //            DependencyGraphGeneratorFramework.IDGGraphExporter exporter = new GraphvizExporter.GraphvizExporter();

            /*
            IDGGraphExporter exporter = new CSVExporter.CSVExporter();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = exporter.GetExtension();
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                exporter.ExportGraph(FileInfos, sfd.FileName);
            }
            */
        }
    }
}
