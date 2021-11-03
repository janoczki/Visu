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
            timer1.Enabled = startBacnet();
            timer2.Enabled = startBacnet();
        }

        private bool startBacnet()
        {
            return Bac.startActivity("192.168.16.57");
        }

        private void readData(List<List<string>> dataTable)
        {
            foreach (List<string> property in dataTable)
            {
                var devIP = property[5];
                var devInst = Convert.ToUInt16(property[6]);
                var objType = property[7];
                var objInst = Convert.ToUInt16(property[8]);
                dataTable[dataTable.IndexOf(property)][9] = Bac.readValue(1, devIP, devInst, objType, objInst, "PV");
                //dataList.Add(Bac.readValue(1, devIP, devInst, objType, objInst, "PV"));
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            readData(_global.bigDatapointTable);
        }

        private void megnyitásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var file = openDatapointFile();

            foreach (string item in file)
            {
                var itemProperty = item.Split(';').ToList();
                itemProperty.Add("");
                _global.bigDatapointTable.Add(itemProperty);

                var itemPropertyArray = itemProperty.ToArray();
                //_global.bigDatapointTable.Add(itemProperty.ToList());
                listView1.Items.Add(new ListViewItem(itemPropertyArray));

                //string[] row = {
                //    itemProperty[0],
                //    itemProperty[1],
                //    itemProperty[2],
                //    itemProperty[3],
                //    itemProperty[4],
                //    itemProperty[5],
                //    itemProperty[6],
                //    itemProperty[7],
                //    itemProperty[8],
                //    ""};
            }
        }

        private void DataViewer_Load(object sender, EventArgs e)
        {
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //var result = (List<string>)e.Result;

            //foreach (string item in result)
            //{
            //    var ind = result;
            //    listView1.Items[result.IndexOf(item)].SubItems[6].Text = item;
            //}
            ////MessageBox.Show("asd");
            ////foreach (ListViewItem item in listView1.Items)

            ////{
            ////    item.SubItems[6].Text = roundedValue.ToString();
            ////}

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            foreach (var item in _global.bigDatapointTable)
            {
                listView1.Items[_global.bigDatapointTable.IndexOf(item)].SubItems[9].Text = item[9];
            }
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
