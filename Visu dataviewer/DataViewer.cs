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

namespace Visu_dataviewer
{
    public partial class DataViewer : Form
    {
        public DataViewer()
        {
            InitializeComponent();
        }

        private string[] openDatapointFile()
        {
            var directory = openProjectDirectory();
            if (directory != null)
            {
                _global.path = directory;
                try
                {
                    string[] file = File.ReadAllLines(_global.path + "\\adatpontok.dp", Encoding.Default);
                    return file;
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("A megadott projektben nem található az adatpontokat definiáló fájl");
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

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Columns.Add("Name", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Desc", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Dev IP", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Dev inst", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Obj type", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Obj inst", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Value", 100, HorizontalAlignment.Center);

            var file = openDatapointFile();

            foreach (string item in file)
            {
                var itemProperty = item.Split(';');
                string[] row = {
                    itemProperty[1],
                    itemProperty[2],
                    itemProperty[6],
                    itemProperty[7],
                    itemProperty[8],
                    itemProperty[9],
                    ""};

                listView1.Items.Add(new ListViewItem(row));
            }

            startBacnet();
            timer1.Enabled = true;
        }

        private void startBacnet()
        {
            Bac.startActivity("192.168.16.57");
            
        }

        private void readData()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                string devIP = item.SubItems[2].Text;
                uint devInst = Convert.ToUInt16(item.SubItems[3].Text);
                string objType = item.SubItems[4].Text;
                uint objInst = Convert.ToUInt16(item.SubItems[5].Text);

                var readedValue = Bac.readValue(1, devIP, devInst, objType, objInst, "PV");
                try
                {
                    var doubleValue = Convert.ToDouble(readedValue);
                    var roundedValue = Math.Round(doubleValue, 1);
                    item.SubItems[6].Text = roundedValue.ToString();
                }
                catch (FormatException)
                {
                    item.SubItems[6].Text = "Nincs adat";
                }

                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            readData();
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
