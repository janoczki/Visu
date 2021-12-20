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
namespace Visu_dataviewer
{
    public partial class DataViewer : Form
    {
        // UNDONE Egyedi polling időzítés lehetőség, nem thread safe, így elvetve
        // TODO logolás indításra
        // TODO logolás leállásra
        // TODO logolás hibára
        // TODO adatvalidálások (adatpont fájl, config fájl, idők)

        public DataViewer()
        {
            InitializeComponent();
        }



        private void DataViewer_Load(object sender, EventArgs e)
        {
            _global.ini();
            Log.Append("Application start");
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addContentToListview();
            UItimer.Enabled = true;
            startProgress();
        }

        public void addHeaders(string[] headers)
        {
            foreach (string header in headers)
            {
                listView1.Columns.Add(header, 1, HorizontalAlignment.Center);
            }
        }

        private string openProjectDirectory()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    return folderBrowserDialog.SelectedPath;
                }
            }
            return null;
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
                    _global.bigDatapointTable.Add(datapoint.ToList());
                    Datapoints.table.Add(datapoint.ToList());
                    listView1.Items.Add(new ListViewItem(datapoint));
                }
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Append("Quit application");
            Application.Exit();
        }






        private void startProgress()
        {
            startBacnet();
            Log.Append("Online mode");
            covSubscriptionTimer.Interval = Convert.ToInt32((_global.covLifetime - 1) * 1000);
            covSubscriptionTimer.Enabled = true;
            Bac.subscribeToAll();
            Log.Append("Subscription started");
            pollingTimer.Interval = int.Parse(pollingIntervalTextbox.Text) * 1000;
            pollingTimer.Enabled = true;
            Bac.readAllValue();
            Log.Append("Pollings started");
        }

        private void covSubscriptionTimer_Tick(object sender, EventArgs e)
        {
            Log.Append("Resubscription started");
            Bac.subscribeToAll();
            Log.Append("Resubscription finished");
        }

        private void onlineButton_Click(object sender, EventArgs e)
        {
            startBacnet();
            Log.Append("Online mode");
            UItimer.Enabled = true;
            pollingButton.Enabled = true;
            subscribeButton.Enabled = true;
        }

        private void subscribeButton_Click(object sender, EventArgs e)
        {
            Bac.subscribeToAll();
            Log.Append("Subscription started");
            covSubscriptionTimer.Interval = Convert.ToInt32((_global.covLifetime - 1) * 1000);
            covSubscriptionTimer.Enabled = true;
        }

        private bool startBacnet()
        {
            return Bac.startActivity("192.168.1.77");
        }

        private void pollingTimer_Tick(object sender, EventArgs e)
        {
            Bac.readAllValue();
        }

        private void UITimer_Tick(object sender, EventArgs e)
        {
            foreach (var row in Datapoints.table)
            {
                listView1.Items[Datapoints.table.IndexOf(row)].SubItems[(int)DatapointDefinition.columns.value].Text = row[(int)DatapointDefinition.columns.value];
            }
        }

        private void pollingButton_Click(object sender, EventArgs e)
        {
            if (pollingButton.Text == "Start polling")
            {
                pollingButton.Text = "Stop polling";
                Log.Append("Start polling");
                pollingTimer.Interval = int.Parse(pollingIntervalTextbox.Text) * 1000;
                pollingTimer.Enabled = true;
                Bac.readAllValue();
            }
            else
            {
                pollingButton.Text = "Start polling";
                Log.Append("Stop polling");
                pollingTimer.Enabled = false;
            }
        }

        private void openReaderWriter(ListViewItem selected)
        {
            var readerwriter = Application.OpenForms["ReaderWriter"] as ReaderWriter;
            if (readerwriter == null)
                readerwriter = new ReaderWriter();
            readerwriter.transferData(selected);
            readerwriter.Show();
        }

        private void openScheduleReaderWriter(ListViewItem selected)
        {
            var schedulereaderwriter = Application.OpenForms["ScheduleReaderWriter"] as ScheduleReaderWriter;
            if (schedulereaderwriter == null)
                schedulereaderwriter = new ScheduleReaderWriter();
            schedulereaderwriter.transferData(selected);
            schedulereaderwriter.Show();
        }

        private List<string> collectWeeklyScheduleByTimeAndCommand(List<string> week)
        {
            var timeAndCommands = new List<string>();

            foreach (string day in week)
            {
                var evs = day.Split(',');
                foreach (string ev in evs)
                {
                    if (ev != "")
                    {
                        if (!timeAndCommands.Contains(ev))
                        {
                            timeAndCommands.Add(ev);
                        }
                        
                    }
                    
                }
            }






            return timeAndCommands;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selected = listView1.SelectedItems[0];
            if (selected.SubItems[(int)DatapointDefinition.columns.objectType].Text == "SC")
            {
                openScheduleReaderWriter(selected);
            }
            else
            {
                openReaderWriter(selected);
            }
            
        }

        private void sqlConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sql.connect();
        }
    }
}
