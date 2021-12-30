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
        public ListViewItem selected;

        public ReaderWriter()
        {
            InitializeComponent();
        }

        public void transferData(ListViewItem selected)
        {
            this.selected = selected;
            nameLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.datapointName].Text;
            descLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.datapointDescription].Text;
            typeLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.datapointDatatype].Text;
            recLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.datapointSave].Text;
            objCovLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.datapointCOV].Text;
            devIPLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.deviceIP].Text;
            devInstLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.deviceInstance].Text;
            objTypeLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.objectType].Text;
            objInstLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.objectInstance].Text;
            readedValueLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.value].Text;
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
            var obj = new BacnetObjects.NormalObject(bacnetDevice, bacnetObject);
            obj.Write(value, format, false);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            var bacnetDevice = Bac.getBacnetDevice(devIPLabel.Text, 1);
            var bacnetObject = Bac.getBacnetObject(objTypeLabel.Text, Convert.ToUInt16(objInstLabel.Text));
            var format = typeLabel.Text;
            var value = valueToWriteTextbox.Text;
            var obj = new BacnetObjects.NormalObject(bacnetDevice, bacnetObject);
            obj.Write("0", format, true);
        }
    }
}
