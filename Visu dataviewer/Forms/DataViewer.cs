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
using System.Timers;
using System.IO.BACnet;
namespace Visu_dataviewer
{
    public partial class DataViewer : Form
    {
        public DataViewer()
        {
            InitializeComponent();
        }
        
        private void DataViewer_Load(object sender, EventArgs e)
        {
            global.ini();
            Log.Append("Application start");
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addContentToListview();
            resizeListViewColumns();
            startProgress();
        }

        public void addHeaders(string[] headers)
        {
            foreach (string header in headers)
            {
                listView1.Columns.Add(header, 1, HorizontalAlignment.Center);
            }
        }

        public void addContentToListview()
        {
            var datapointTable = DatapointDefinition.getTable();
            if (datapointTable != null)
            {
                addHeaders(DatapointDefinition.header);
                foreach (string row in datapointTable)
                {
                    var datapoint = (row + ";").Split(';');
                    Datapoints.table.Add(datapoint.ToList());
                    listView1.Items.Add(new ListViewItem(datapoint));
                }
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        public void resizeListViewColumns()
        {
            for (int i = (int)DatapointDefinition.columns.txt00;i < (int)DatapointDefinition.columns.value; i++)
            {
                listView1.Columns[i].Width = 0;
            }
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Append("Quit application");
            Application.Exit();
        }
        
        private void startProgress()
        {
            UItimer.Enabled = true;
            Bac.startActivity("192.168.1.77");
            covSubscriptionTimer.Interval = Convert.ToInt32((global.covLifetime - 1) * 1000);
            covSubscriptionTimer.Enabled = true;
            Bac.subscribeToAll();
            pollingTimer.Interval = global.pollInterval;
            pollingTimer.Enabled = true;
            Bac.readAll();
        }

        private void covSubscriptionTimer_Tick(object sender, EventArgs e)
        {
            Bac.subscribeToAll();
        }

        private void pollingTimer_Tick(object sender, EventArgs e)
        {
            Bac.readAll();
        }

        private void UITimer_Tick(object sender, EventArgs e)
        {
            foreach (var row in Datapoints.table)
            {
                listView1.Items[Datapoints.table.IndexOf(row)].SubItems[(int)DatapointDefinition.columns.value].Text = row[(int)DatapointDefinition.columns.value];
            }
        }
        
        private bool openReaderWriter(ListViewItem selected)
        {
            var rw = Application.OpenForms["ReaderWriter"] as ReaderWriter;
            if (rw == null) rw = new ReaderWriter();
            rw.transferData(selected);
            rw.Show();
            return true;
        }

        private bool openScheduleReaderWriter(ListViewItem selected)
        {
            var srw = Application.OpenForms["ScheduleReaderWriter"] as ScheduleReaderWriter;
            if (srw == null) srw = new ScheduleReaderWriter();
            srw.transferData(selected);
            var actionType = selected.SubItems[(int)DatapointDefinition.columns.txt01].Text == "" ? "value" : "enum";
            var possibleCommands = srw.collectPossibleCommands(selected);
            srw.addHeaders(actionType, possibleCommands);
            var schedule = srw.parseSchedule(selected);
            srw.representSchedule(schedule);
            srw.Show();
            return true;
        }
        
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selected = listView1.SelectedItems[0];
            var isSchedule = selected.SubItems[(int)DatapointDefinition.columns.objectType].Text == "SC";
            var operation = isSchedule ? openScheduleReaderWriter(selected) : openReaderWriter(selected);
        }

        private void DataViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.Append("Application stop");
        }
    }
}
