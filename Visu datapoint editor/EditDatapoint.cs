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
    }
}
