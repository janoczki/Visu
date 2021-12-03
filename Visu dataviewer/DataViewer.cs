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

        private string[] openDatapointFile()
        {
            Log.append("Opening datapoint file");
            //var directory = openProjectDirectory();
            var directory = _global.path;
            if (directory != null)
            {
                //_global.path = directory;
                try
                {
                    string[] file = File.ReadAllLines(_global.path + "\\datapoints.bacnetip", Encoding.Default);
                    return file;
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("The file defining Bacnet IP objects doesn't exist.");
                }
            }
            return null;
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

        private void onlineButton_Click(object sender, EventArgs e)
        {
            startBacnet();
            Log.append("Online mode");
            UItimer.Enabled = true;
            pollingButton.Enabled = true;
            subscribeButton.Enabled = true;
        }

        private void subscribeButton_Click(object sender, EventArgs e)
        {
            covSubscriptionTimer.Enabled = true;
        }

        private bool startBacnet()
        {
            return Bac.startActivity("192.168.16.57");
        }

        private void pollingTimer_Tick(object sender, EventArgs e)
        {
            Log.append("Polling timer tick");
            Bac.poll();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var file = openDatapointFile();
            foreach (string item in file)
            {
                var itemProperty = item.Split(';').ToList();
                itemProperty.Add(""); // value
                itemProperty.Add(""); // active text
                itemProperty.Add(""); // inactive text
                itemProperty.Add(""); // multistate statuses
                itemProperty.Add(""); // availability

                itemProperty.Add(""); // Monday
                itemProperty.Add(""); // Tuesday
                itemProperty.Add(""); // Wednesday
                itemProperty.Add(""); // Thursday
                itemProperty.Add(""); // Friday
                itemProperty.Add(""); // Saturday
                itemProperty.Add(""); // Sunday

                _global.bigDatapointTable.Add(itemProperty);
                var itemPropertyArray = itemProperty.ToArray();
                listView1.Items.Add(new ListViewItem(itemPropertyArray));
            }

            Sql.connect();
            Bac.startActivity("192.168.16.57");
            Bac.checkAvailability();

            while (!Bac.availabilityCheckComplete)
            {
                Application.DoEvents();
            }
            Bac.subscribe();
            Bac.readStates();

            foreach (var item in _global.bigDatapointTable)
            {
                listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.activeText].Text = item[(int)_global.prop.activeText];
                listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.inactiveText].Text = item[(int)_global.prop.inactiveText];
                listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.stateText].Text = item[(int)_global.prop.stateText];
                listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.available].Text = item[(int)_global.prop.available];


            }

            UItimer.Enabled = true;

            foreach (ColumnHeader column in listView1.Columns)
            {
                column.Width = -2;
            }

            for (int i = 10; i < 14; i++)
            {
                listView1.Columns[i].Width = 0;
            }
            

        }

        public static void correctHeaders()
        {
            
        }
        private void DataViewer_Load(object sender, EventArgs e)
        {
            _global.ini();
            Log.append("Application start");
            listView1.Columns.Add("asd", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Object name", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Object description", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Object datatype", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Save", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Change of value", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Device IP", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Device instance", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Object type", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Object instance", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Present value", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("Active text", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Inactive text", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Multistate statuses", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Availability", 0, HorizontalAlignment.Center);

            listView1.Columns.Add("Monday", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Tuesday", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Wednesday", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Thursday", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Friday", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Saturday", 0, HorizontalAlignment.Center);
            listView1.Columns.Add("Sunday", 0, HorizontalAlignment.Center);

            listView1.Columns.RemoveAt(0);
            //itemProperty.Add(""); // value
            //itemProperty.Add(""); // active text
            //itemProperty.Add(""); // inactive text
            //itemProperty.Add(""); // multistate statuses
            //itemProperty.Add(""); // availability
        }

        private void UITimer_Tick(object sender, EventArgs e)
        {
            var permission = _global.cycleCounter % 10 == 0;

            foreach (var item in _global.bigDatapointTable)
            {
                Application.DoEvents();
                listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.value].Text = item[(int)_global.prop.value];


                if (permission)
                {
                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.activeText].Text = item[(int)_global.prop.activeText];
                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.inactiveText].Text = item[(int)_global.prop.inactiveText];
                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.stateText].Text = item[(int)_global.prop.stateText];
                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.available].Text = item[(int)_global.prop.available];

                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.dayMo].Text = item[(int)_global.prop.dayMo];
                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.dayTu].Text = item[(int)_global.prop.dayTu];
                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.dayWe].Text = item[(int)_global.prop.dayWe];
                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.dayTh].Text = item[(int)_global.prop.dayTh];
                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.dayFr].Text = item[(int)_global.prop.dayFr];
                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.daySa].Text = item[(int)_global.prop.daySa];
                    listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[(int)_global.prop.daySu].Text = item[(int)_global.prop.daySu];
                    _global.cycleCounter = 1;
                }

            }

            _global.cycleCounter++;
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.append("Quit application");
            Application.Exit();
        }

        private void covSubscriptionTimer_Tick(object sender, EventArgs e)
        {
            Log.append("Subscriber timer tick");

            if (covSubscriptionTimer.Interval == 100)
            {
                covSubscriptionTimer.Interval = Convert.ToInt32((_global.covLifetime - 1) * 1000);
            }
            Bac.subscribe();
        }

        private void pollingButton_Click(object sender, EventArgs e)
        {
            if (pollingButton.Text == "Start polling")
            {
                pollingButton.Text = "Stop polling";
                Log.append("Start polling");
                pollingTimer.Interval = int.Parse(pollingIntervalTextbox.Text) * 1000;
                pollingTimer.Enabled = true;
                Bac.poll();
            }
            else
            {
                pollingButton.Text = "Start polling";
                Log.append("Stop polling");
                pollingTimer.Enabled = false;
            }
        }

        private void openReaderWriter(ListViewItem selected)
        {
            var edit = Application.OpenForms["ReaderWriter"] as ReaderWriter;

            if (edit == null)
            {
                edit = new ReaderWriter();
            }
            edit.selected = selected;
            edit.nameLabel.Text = selected.SubItems[(int)_global.prop.datapointName].Text;
            edit.descLabel.Text = selected.SubItems[(int)_global.prop.datapointDescription].Text;
            edit.typeLabel.Text = selected.SubItems[(int)_global.prop.datapointDatatype].Text;
            edit.recLabel.Text = selected.SubItems[(int)_global.prop.datapointSave].Text;
            edit.objCovLabel.Text = selected.SubItems[(int)_global.prop.datapointCOV].Text;
            edit.devIPLabel.Text = selected.SubItems[(int)_global.prop.deviceIP].Text;
            edit.devInstLabel.Text = selected.SubItems[(int)_global.prop.deviceInstance].Text;
            edit.objTypeLabel.Text = selected.SubItems[(int)_global.prop.objectType].Text;
            edit.objInstLabel.Text = selected.SubItems[(int)_global.prop.objectInstance].Text;
            edit.readedValueLabel.Text = selected.SubItems[(int)_global.prop.value].Text;
            edit.Show();
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

            if (selected.SubItems[(int)_global.prop.objectType].Text == "SC")
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

        private void button1_Click(object sender, EventArgs e)
        {
            var asd = Bac.readValue(1, "192.168.16.156", 156, "SC", 0, "Weekly");
            Console.WriteLine(asd);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bac.writeSchedule(1, "192.168.16.156", 156, "SC", 0, "Weekly");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sch = new ScheduleReaderWriter();
            sch.Show();
           
        }
    }

    public class ListViewNF : System.Windows.Forms.ListView
    {
        public ListViewNF()
        {
            //Activate double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
    }
}
