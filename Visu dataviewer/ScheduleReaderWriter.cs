using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.BACnet;
namespace Visu_dataviewer
{
    
    public partial class ScheduleReaderWriter : Form
    {
        public ListViewItem selected;
        public ScheduleReaderWriter()
        {
            InitializeComponent();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.ColumnIndex.ToString() + e.RowIndex.ToString() + e.Exception);
        }

        public void transferData(ListViewItem selected)
        {
            var bacnetDevice = Bac.getBacnetDevice(selected.SubItems[(int)DatapointDefinition.columns.deviceIP].Text,1);
            var bacnetObject = Bac.getBacnetObject(selected.SubItems[(int)DatapointDefinition.columns.objectType].Text, Convert.ToUInt16(selected.SubItems[(int)DatapointDefinition.columns.objectInstance].Text));
            var asd = Bac.readSchedule(bacnetDevice, bacnetObject);
            this.selected = selected;
        }

        private void addHeaders(string type)
        {
            var timeColumn = new DataGridViewTextBoxColumn() { Name = "Time", HeaderText = "Time", AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader };
            DataGridViewColumn actionColumn;

            if (type == "enum")
            {
                actionColumn = new DataGridViewComboBoxColumn() { Name = "Action", HeaderText = "Action", AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader };
            }
            else
            {
                actionColumn = new DataGridViewTextBoxColumn() { Name = "Action", HeaderText = "Action", AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader };
            }
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { timeColumn, actionColumn });
            var days = new string[] { "Mo", "tu", "We", "Th", "Fr", "Sa", "Su" };
            foreach (string day in days)
            {
                dataGridView2.Columns.Add(new DataGridViewCheckBoxColumn() { Name = day, HeaderText = day, AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader });
            }
        }

        private void ScheduleReaderWriter_Load(object sender, EventArgs e)
        {
            var stateText00 = selected.SubItems[(int)DatapointDefinition.columns.txt00].Text;
            var actionType = stateText00 == "" ? "value" : "enum";
            addHeaders(actionType);
        }
    }
}
