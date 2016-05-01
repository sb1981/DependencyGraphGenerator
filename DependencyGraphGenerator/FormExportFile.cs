using DependencyGraphGeneratorFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DependencyGraphGenerator
{
    public partial class FormExportFile : Form
    {
        private IDGGraphExporter[] exporters;
        private FileInfo[] fileInfos;

        public FormExportFile(IDGGraphExporter[] exporters, FileInfo[] fileInfos)
        {
            this.exporters = exporters;
            this.fileInfos = fileInfos;

            InitializeComponent();

            foreach(IDGGraphExporter e in exporters)
            {
                listBoxExporters.Items.Add(e.GetName());
            }
            listBoxExporters.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            IDGGraphExporter exporter = null;
            foreach (IDGGraphExporter ex in exporters)
            {
                if(ex.GetName() == listBoxExporters.SelectedItem.ToString())
                {
                    exporter = ex;
                    break;
                }
            }
            if(exporter==null)
            {
                MessageBox.Show("Error getting exporter", "Error");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = exporter.GetExtension();
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                if(exporter.ExportGraph(fileInfos, sfd.FileName)!=0)
                {
                    MessageBox.Show("Could not export the file", "Error");
                }
                else
                {
                    MessageBox.Show("File successfully exported");
                }
            }
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBoxExporters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
