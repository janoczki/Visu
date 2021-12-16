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
            return Bac.startActivity("192.168.16.57");
        }

        private void pollingTimer_Tick(object sender, EventArgs e)
        {
            Log.Append("Polling timer tick");
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
            {
                readerwriter = new ReaderWriter();
            }
            readerwriter.selected = selected;
            readerwriter.nameLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.datapointName].Text;
            readerwriter.descLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.datapointDescription].Text;
            readerwriter.typeLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.datapointDatatype].Text;
            readerwriter.recLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.datapointSave].Text;
            readerwriter.objCovLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.datapointCOV].Text;
            readerwriter.devIPLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.deviceIP].Text;
            readerwriter.devInstLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.deviceInstance].Text;
            readerwriter.objTypeLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.objectType].Text;
            readerwriter.objInstLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.objectInstance].Text;
            readerwriter.readedValueLabel.Text = selected.SubItems[(int)DatapointDefinition.columns.value].Text;
            readerwriter.Show();
        }

        private void openScheduleReaderWriter(ListViewItem selected)
        {
            var offsetDueToMultistate = 0;
            var edit = Application.OpenForms["ScheduleReaderWriter"] as ScheduleReaderWriter;

            if (edit == null)
            {
                edit = new ScheduleReaderWriter();
            }

            edit.dataGridView1.Rows.Clear();
            var week = new List<string>
            {
                selected.SubItems[(int)_global.prop.dayMo].Text,
                selected.SubItems[(int)_global.prop.dayTu].Text,
                selected.SubItems[(int)_global.prop.dayWe].Text,
                selected.SubItems[(int)_global.prop.dayTh].Text,
                selected.SubItems[(int)_global.prop.dayFr].Text,
                selected.SubItems[(int)_global.prop.daySa].Text,
                selected.SubItems[(int)_global.prop.daySu].Text,
            };

            var possibleCommands = new DataGridViewComboBoxCell();
            if (selected.SubItems[(int)_global.prop.stateText].Text == "")
            {
                possibleCommands.Items.Add(selected.SubItems[(int)_global.prop.inactiveText].Text);
                possibleCommands.Items.Add(selected.SubItems[(int)_global.prop.activeText].Text);
            }
            else
            {
                var commands = selected.SubItems[(int)_global.prop.stateText].Text.Split(',');

                foreach (var command in commands)
                {
                    possibleCommands.Items.Add(command);
                }
                offsetDueToMultistate = 1;
            }

            var scheduleTimeAndCommands = collectWeeklyScheduleByTimeAndCommand(week);

            var rowsAdded = 0;
            foreach (string timeAndCommand in scheduleTimeAndCommands)
            {
                var timeAndCommandArray = timeAndCommand.Split(':');
                var hour = _global.normalizeNumber(timeAndCommandArray[0],2);
                var minute = _global.normalizeNumber(timeAndCommandArray[1], 2);
                var second = _global.normalizeNumber(timeAndCommandArray[2], 2);
                var time = hour + ":" + minute + ":" + second;
                var command = timeAndCommandArray[4];

                
                edit.dataGridView1.Rows.Add(time, "", "", false, false, false, false, false, false, false);

                var indexOfDay = 0;

                var actions = (DataGridViewComboBoxCell)edit.dataGridView1.Rows[rowsAdded].Cells[1];
                foreach (var action in possibleCommands.Items)
                {
                    actions.Items.Add(action);
                }
                actions.Value = possibleCommands.Items[int.Parse(command) - offsetDueToMultistate];

                foreach (string day in week)
                {
                    if (day.Contains(timeAndCommand))
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)edit.dataGridView1.Rows[rowsAdded].Cells[week.IndexOf(day) + 3];
                        edit.dataGridView1.Rows[rowsAdded].Cells[indexOfDay + 3].Value = true;
                    }
                    indexOfDay++;
                }
                rowsAdded++;
            }
            edit.Show();
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
                return;
            }
            openReaderWriter(selected);
        }

        private void sqlConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sql.connect();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
