using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DependencyGraphGenerator
{
    public partial class FormOpenFile : Form
    {
        private List<string> fileNames;
        private List<string> vendorsToExclude;
        private bool isOKPressed = false;

        public FormOpenFile()
        {
            InitializeComponent();
        }

        public bool ShowDialog(List<string> fileNames, List<string> vendorsToExclude)
        {
            this.fileNames = fileNames;
            this.vendorsToExclude = vendorsToExclude;
            base.ShowDialog();
            return isOKPressed;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(textBoxFiles.Text == "")
            {
                MessageBox.Show("No file(s) selected", "Error");
                return;
            }
            fileNames.AddRange(textBoxFiles.Text.Split(new char[] { ';' }));
            if (textBoxExcludeVendors.Text != "")
            {
                vendorsToExclude.AddRange(textBoxExcludeVendors.Text.Split(new char[] { ';' }));
            }
            isOKPressed = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSelectFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxFiles.Text = string.Join("; ", ofd.FileNames);
            }
        }

        private void buttonFileList_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files with 1 file per line (*.txt)|*.txt|CSV files with ';' separator (*.csv)|*.csv";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> files = new List<string>();
                if (Path.GetExtension(ofd.FileName).ToLower()==".csv")
                {
                    using (StreamReader reader = new StreamReader(File.OpenRead(ofd.FileName)))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(';');
                            files.AddRange(values);
                        }
                    }
                }
                else if(Path.GetExtension(ofd.FileName).ToLower() == ".txt")
                {
                    using (StreamReader reader = new StreamReader(File.OpenRead(ofd.FileName)))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            files.Add(line);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Unsupported extension", "Error");
                }
                textBoxFiles.Text = string.Join("; ", files);
            }
        }

        private void textBoxFiles_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelExecutables_Click(object sender, EventArgs e)
        {

        }
    }
}
