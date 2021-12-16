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
            readedValueLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.value].Text.ToString();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            var bacnetDevice = Bac.getBacnetDevice(devIPLabel.Text, 1);
            var bacnetObject = Bac.getBacnetObject(objTypeLabel.Text, Convert.ToUInt16(objInstLabel.Text));
            var value = valueToWriteTextbox.Text;
            var format = typeLabel.Text;
            Bac.lofasz(bacnetDevice, bacnetObject, value, format, false);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            var bacnetDevice = Bac.getBacnetDevice(devIPLabel.Text, 1);
            var bacnetObject = Bac.getBacnetObject(objTypeLabel.Text, Convert.ToUInt16(objInstLabel.Text));
            var format = typeLabel.Text;
            var value = valueToWriteTextbox.Text;
            Bac.lofasz(bacnetDevice, bacnetObject, value, format, true);
        }
    }
}
