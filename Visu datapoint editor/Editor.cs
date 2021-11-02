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
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }

        private void megnyitásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] file = openDatapointFile();
            if (file != null)
            {
                fileToDatagrid(file, dataGridView1);
                
            }
        }

        private string[] openDatapointFile()
        {
            var directory = openProjectDirectory();
            

            if (directory != null)
            {
                _global.path = directory;
                try
                {
                    string[] file = File.ReadAllLines(_global.path + "\\adatpontok.dp", Encoding.Default);
                    return file;
                }

                catch (FileNotFoundException)
                {
                    MessageBox.Show("A megadott projektben nem található az adatpontokat definiáló fájl");
                    dataGridView1.Rows.Clear();
                }

            }
            return null;
            
        }

        private string openProjectDirectory()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    return folderBrowserDialog.SelectedPath;
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
                datagrid.Rows.Add(datapointData);
            }
        }

        public void createDatagridViewHeader (DataGridView datagrid)
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

        private void button1_Click(object sender, EventArgs e)
        {
            openEdit();
        }

        private void openEdit()
        {
            var edit = Application.OpenForms["EditDatapoint"] as EditDatapoint;

            if (edit == null)
            {
                edit = new EditDatapoint();
            }
            sendDataToEdit(edit);
            edit.Show();
        }

        private void sendDataToEdit(EditDatapoint edit)
        {
            
            var rIndex = dataGridView1.CurrentCell.RowIndex;
            var cIndex = dataGridView1.CurrentCell.ColumnIndex;
            if (rIndex <= dataGridView1.RowCount-2)
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.Rows[rIndex].Selected = true;
                edit.textBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
                edit.textBox2.Text = dataGridView1.SelectedCells[2].Value.ToString();
                edit.comboBox1.Text = dataGridView1.SelectedCells[3].Value.ToString();
                edit.checkBox1.Checked = bool.Parse(dataGridView1.SelectedCells[4].Value.ToString());
                edit.checkBox2.Checked = bool.Parse(dataGridView1.SelectedCells[5].Value.ToString());
                edit.textBox3.Text = dataGridView1.SelectedCells[6].Value.ToString();
                edit.textBox4.Text = dataGridView1.SelectedCells[7].Value.ToString();
                edit.comboBox2.Text = dataGridView1.SelectedCells[8].Value.ToString();
                edit.textBox5.Text = dataGridView1.SelectedCells[9].Value.ToString();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dataGridView1.CurrentCell = dataGridView1[cIndex, rIndex];
            }

        }

        private void mentésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = _global.path + "\\adatpontok.dp";
            var content = collectDataToSave();
            File.WriteAllText(path, content);
        }

        private string collectDataToSave()
        {
            var collection = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index < dataGridView1.RowCount - 1)
                {
                    for (int i = 0; i <= 8; i++)
                    {
                        collection = collection + row.Cells[i].Value + ";";
                    }
                    collection = collection + row.Cells[9].Value + "\n";
                }
            }
            return collection;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            openEdit();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //contextMenuStrip1.Show(dataGridView1, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            }

            catch (InvalidOperationException)
            {
                MessageBox.Show("A kijelölt sorban még nincs semmi, így az nem törölhető!"); 
            }
            //dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;
            
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView1[0, dataGridView1.Rows.Count-1].Value = _global.maxID + 1;
            _global.maxID++;
        }

        private void bezárásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void újToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newPrj = Application.OpenForms["New"] as New;

            if (newPrj == null)
            {
                newPrj = new New();
            }
            newPrj.Show();
        }
    }
}
