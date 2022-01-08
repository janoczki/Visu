using System;
using System.Windows.Forms;

namespace Visu_dataviewer.Forms
{
    public partial class ReaderWriter : Form
    {
        public ListViewItem Selected;

        public ReaderWriter()
        {
            InitializeComponent();
        }

        public void TransferData(ListViewItem selected)
        {
            this.Selected = selected;
            nameLabel.Text = selected.SubItems[(int)DatapointDefinition.Columns.DatapointName].Text;
            descLabel.Text = selected.SubItems[(int)DatapointDefinition.Columns.DatapointDescription].Text;
            typeLabel.Text = selected.SubItems[(int)DatapointDefinition.Columns.DatapointDatatype].Text;
            recLabel.Text = selected.SubItems[(int)DatapointDefinition.Columns.DatapointSave].Text;
            objCovLabel.Text = selected.SubItems[(int)DatapointDefinition.Columns.DatapointCov].Text;
            devIPLabel.Text = selected.SubItems[(int)DatapointDefinition.Columns.DeviceIp].Text;
            devInstLabel.Text = selected.SubItems[(int)DatapointDefinition.Columns.DeviceInstance].Text;
            objTypeLabel.Text = selected.SubItems[(int)DatapointDefinition.Columns.ObjectType].Text;
            objInstLabel.Text = selected.SubItems[(int)DatapointDefinition.Columns.ObjectInstance].Text;
            readedValueLabel.Text = selected.SubItems[(int)DatapointDefinition.Columns.Value].Text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            readedValueLabel.Text = Selected.SubItems[(int)DatapointDefinition.Columns.Value].Text.ToString();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            var bacnetDevice = Bac.GetBacnetDevice(devIPLabel.Text, 1);
            var bacnetObject = Bac.GetBacnetObject(objTypeLabel.Text, Convert.ToUInt16(objInstLabel.Text));
            var value = valueToWriteTextbox.Text;
            var format = typeLabel.Text;
            var obj = new BacnetObjects.NormalObject(bacnetDevice, bacnetObject);
            obj.Write(value, format, false);
            Datapoints.Record(bacnetDevice, bacnetObject, value);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            var bacnetDevice = Bac.GetBacnetDevice(devIPLabel.Text, 1);
            var bacnetObject = Bac.GetBacnetObject(objTypeLabel.Text, Convert.ToUInt16(objInstLabel.Text));
            var format = typeLabel.Text;
            var obj = new BacnetObjects.NormalObject(bacnetDevice, bacnetObject);
            obj.Write("0", format, true);
            Datapoints.Record(bacnetDevice, bacnetObject, obj.Read());
        }
    }
}
