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
            {
                Bac.resetValue(
                    1,
                    devIPLabel.Text,
                    devInstLabel.Text,
                    objTypeLabel.Text,
                    Convert.ToUInt16(objInstLabel.Text)
                    );
            }
        }
    }
}
