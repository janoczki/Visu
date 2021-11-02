using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visu_datapoint_editor
{
    public partial class EditDatapoint : Form
    {
        public EditDatapoint()
        {
            InitializeComponent();
        }

        private void EditDatapoint_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("int");
            comboBox1.Items.Add("uint");
            comboBox1.Items.Add("float");
            comboBox1.Items.Add("binary");
            comboBox1.Items.Add("string");

            comboBox2.Items.Add("AI");
            comboBox2.Items.Add("AO");
            comboBox2.Items.Add("AV");
            comboBox2.Items.Add("BI");
            comboBox2.Items.Add("BO");
            comboBox2.Items.Add("BV");
            comboBox2.Items.Add("MI");
            comboBox2.Items.Add("MO");
            comboBox2.Items.Add("MV");
            comboBox2.Items.Add("SC");
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            sendDataToEditor();
        }

        private void sendDataToEditor()

        {
            var Editor = Application.OpenForms["Editor"] as Editor;
            var rowIndex = Editor.dataGridView1.CurrentCell.RowIndex;
            Editor.dataGridView1[1, rowIndex].Value = textBox1.Text;
            Editor.dataGridView1[2, rowIndex].Value = textBox2.Text;
            Editor.dataGridView1[3, rowIndex].Value = comboBox1.Text;
            Editor.dataGridView1[4, rowIndex].Value = checkBox1.Checked;
            Editor.dataGridView1[5, rowIndex].Value = checkBox2.Checked;
            Editor.dataGridView1[6, rowIndex].Value = textBox3.Text;
            Editor.dataGridView1[7, rowIndex].Value = textBox4.Text;
            Editor.dataGridView1[8, rowIndex].Value = comboBox2.Text;
            Editor.dataGridView1[9, rowIndex].Value = textBox5.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
