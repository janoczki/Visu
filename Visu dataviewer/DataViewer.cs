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
                    MessageBox.Show("A megadott projektben nem található a Bacnet IP adatpontokat definiáló fájl");
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
                itemProperty.Add("");
                itemProperty.Add("");
                _global.bigDatapointTable.Add(itemProperty);
                var itemPropertyArray = itemProperty.ToArray();
                listView1.Items.Add(new ListViewItem(itemPropertyArray));
            }

            Sql.connect();
            Bac.startActivity("192.168.16.57");
            Bac.subscribe();


            UItimer.Enabled = true;
        }

        private void DataViewer_Load(object sender, EventArgs e)
        {
            _global.ini();
            Log.append("Application start");
            listView1.Columns.Add("Object name", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Object description", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Object datatype", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Save", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Change of value", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Device IP", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Device instance", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Object type", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Object instance", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Present value", 100, HorizontalAlignment.Center);
        }

        private void UITimer_Tick(object sender, EventArgs e)
        {
            foreach (var item in _global.bigDatapointTable)
            {
                listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[9].Text = item[9];
            }
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



        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(listView1.SelectedItems[0].SubItems[0].Text);
            var edit = Application.OpenForms["ReaderWriter"] as ReaderWriter;

            if (edit == null)
            {
                edit = new ReaderWriter();
            }
            edit.selected = listView1.SelectedItems[0];
            edit.nameLabel.Text = listView1.SelectedItems[0].SubItems[0].Text;
            edit.descLabel.Text = listView1.SelectedItems[0].SubItems[1].Text;
            edit.typeLabel.Text = listView1.SelectedItems[0].SubItems[2].Text;
            edit.recLabel.Text = listView1.SelectedItems[0].SubItems[3].Text;
            edit.objCovLabel.Text = listView1.SelectedItems[0].SubItems[4].Text;
            edit.devIPLabel.Text = listView1.SelectedItems[0].SubItems[5].Text;
            edit.devInstLabel.Text = listView1.SelectedItems[0].SubItems[6].Text;
            edit.objTypeLabel.Text = listView1.SelectedItems[0].SubItems[7].Text;
            edit.objInstLabel.Text = listView1.SelectedItems[0].SubItems[8].Text;
            edit.readedValueLabel.Text = listView1.SelectedItems[0].SubItems[9].Text;
            edit.Show();
            
        }

        private void sqlConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sql.connect();
        }
    }

    class ListViewNF : System.Windows.Forms.ListView
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
