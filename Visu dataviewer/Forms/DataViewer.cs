using System;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

namespace Visu_dataviewer.Forms
{
    public partial class DataViewer : Form
    {
        public DataViewer()
        {
            InitializeComponent();
        }
        
        private void DataViewer_Load(object sender, EventArgs e)
        {
            global.Ini();
            Log.Append("Application start");
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddContentToListview();
            ResizeListViewColumns();
            StartProgress();
        }

        public void AddHeaders(string[] headers)
        {
            foreach (string header in headers)
            {
                listView1.Columns.Add(header, 1, HorizontalAlignment.Center);
            }
        }

        public void AddContentToListview()
        {
            var datapointTable = DatapointDefinition.GetTable();
            if (datapointTable != null)
            {
                AddHeaders(DatapointDefinition.Header);
                foreach (string row in datapointTable)
                {
                    var datapoint = (row + ";").Split(';');
                    Datapoints.Table.Add(datapoint.ToList());
                    listView1.Items.Add(new ListViewItem(datapoint));
                }
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        public void ResizeListViewColumns()
        {
            for (int i = (int)DatapointDefinition.Columns.Txt00;i < (int)DatapointDefinition.Columns.Value; i++)
            {
                listView1.Columns[i].Width = 0;
            }
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Append("Quit application");
            Application.Exit();
        }
        
        private void StartProgress()
        {
            Bac.StartActivity(global.LocalEndPoint, global.WritePriority);
            UItimer.Enabled = true;
            covSubscriptionTimer.Enabled = true;
            pollingTimer.Enabled = true;
        }

        private void covSubscriptionTimer_Tick(object sender, EventArgs e)
        {
            covSubscriptionTimer.Interval = Convert.ToInt32((global.CovLifetime - 1) * 1000);
            var subscriber = new BackgroundWorker();
            subscriber.DoWork += Workers.Subscriber.Work;
            subscriber.RunWorkerCompleted += Workers.Subscriber.Completed;
            subscriber.RunWorkerAsync();
        }

        private void pollingTimer_Tick(object sender, EventArgs e)
        {
            pollingTimer.Interval = global.PollInterval;
            var reader = new BackgroundWorker();
            reader.DoWork += Workers.Reader.Work;
            reader.RunWorkerCompleted += Workers.Reader.Completed;
            reader.RunWorkerAsync();
        }

        private void UITimer_Tick(object sender, EventArgs e)
        {
            foreach (var row in Datapoints.Table)
            {
                listView1.Items[Datapoints.Table.IndexOf(row)].SubItems[(int)DatapointDefinition.Columns.Value].Text = row[(int)DatapointDefinition.Columns.Value];
            }
        }
        
        private static bool OpenReaderWriter(ListViewItem selected)
        {
            var rw = Application.OpenForms["ReaderWriter"] as ReaderWriter ?? new ReaderWriter();
            rw.TransferData(selected);
            rw.Show();
            return true;
        }

        private static bool OpenScheduleReaderWriter(ListViewItem selected)
        {
            var bacnetObject = new BacnetObjects.ScheduleObject(
                Bac.GetBacnetDevice(selected.SubItems[(int)DatapointDefinition.Columns.DeviceIp].Text.ToString(), 1),
                Bac.GetBacnetObject(selected.SubItems[(int)DatapointDefinition.Columns.ObjectType].Text.ToString(), 
                uint.Parse(selected.SubItems[(int)DatapointDefinition.Columns.ObjectInstance].Text.ToString())));
            var schedule = bacnetObject.Read();
            var actions = bacnetObject.CollectActions();
            var possibleCommands = bacnetObject.CollectPossibleCommands(selected);
            var actionType = selected.SubItems[(int)DatapointDefinition.Columns.Txt01].Text == "" ? "value" : "enum";
            var srw = Application.OpenForms["ScheduleReaderWriter"] as ScheduleReaderWriter ?? 
                new ScheduleReaderWriter(selected,schedule, actions, possibleCommands,actionType);
            srw.Show();
            return true;
        }
        
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selected = listView1.SelectedItems[0];
            var isSchedule = selected.SubItems[(int)DatapointDefinition.Columns.ObjectType].Text == "SC";
            var operation = isSchedule ? OpenScheduleReaderWriter(selected) : OpenReaderWriter(selected);
        }

        private void DataViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.Append("Application stop");
            while (Log.inProgress) { };
        }
    }
}
