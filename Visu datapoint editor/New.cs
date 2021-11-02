using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Visu_datapoint_editor
{
    public partial class New : Form
    {
        public New()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            rootDirectoryTextbox.Text = browse();
        }

        private string browse()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    return folderBrowserDialog.SelectedPath;
                }
                return null;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (prjNameTextbox.Text != null)
            {
                _global.path = rootDirectoryTextbox.Text + "\\" + prjNameTextbox.Text;
                Directory.CreateDirectory(_global.path);

                var editor = Application.OpenForms["Editor"] as Editor;
                editor.createDatagridViewHeader(editor.dataGridView1);
                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
