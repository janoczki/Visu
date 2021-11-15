using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visu_dataviewer
{
    public partial class ReaderWriter : Form
    {
        public ReaderWriter()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            readedValueLabel.Text = selected.SubItems[9].Text.ToString();

            if (readedValueLabel.Text != "0" && readedValueLabel.Text != "1")
            {
                var asd = _global.bigDatapointTable;
            }
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Tag != null)
                {
                    if (selected.SubItems[0].Text == ctrl.Tag.ToString())
                    {
                        ctrl.BackColor = selected.SubItems[9].Text == "1" ? Color.Green : Color.Gray;
                        //if (selected.SubItems[9].Text == "1") ? ctrl.BackColor = Color.Green : ctrl.BackColor = Color.Gray ;
                    }
                }

            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            Bac.writeValue(
                1,
                devIPLabel.Text,
                devInstLabel.Text, 
                objTypeLabel.Text, 
                Convert.ToUInt16(objInstLabel.Text), 
                Convert.ToInt16(valueToWriteTextbox.Text)
                );
        }

        private void resetButton_Click(object sender, EventArgs e)
        {

            Bac.writeValue(
                1,
                devIPLabel.Text,
                devInstLabel.Text,
                objTypeLabel.Text,
                Convert.ToUInt16(objInstLabel.Text),
                null
                );

        }
    }
}
