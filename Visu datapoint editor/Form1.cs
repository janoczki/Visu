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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void megnyitásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openDatapointFile() != null)
            {
                fileToDatagrid(openDatapointFile(), dataGridView1);
            }
        }

        private string[] openDatapointFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "DP fájlok (*.DP)|*.DP";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] file = File.ReadAllLines(openFileDialog.FileName, Encoding.Default);
                    return file;
                }
            }

            return null;
            
        }

        private void fileToDatagrid(string[] file, DataGridView datagrid)
        {
            createDatagridViewHeader(datagrid);
            foreach (string datapoint in file)
            {
                string[] datapointData = datapoint.Split(';');
                //var datagridViewItem = new daata(datapointData);
                datagrid.Rows.Add(datapointData);
            }
        }

        private void createDatagridViewHeader (DataGridView datagrid)
        {
            datagrid.ColumnCount = 10;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Object name";
            dataGridView1.Columns[2].Name = "Object description";
            dataGridView1.Columns[3].Name = "Object datatype";
            dataGridView1.Columns[4].Name = "Save";
            dataGridView1.Columns[5].Name = "Change of value";
            dataGridView1.Columns[6].Name = "Device IP";
            dataGridView1.Columns[7].Name = "Device instance";
            dataGridView1.Columns[8].Name = "Object type";
            dataGridView1.Columns[9].Name = "Object instance";
        }
    }
}
