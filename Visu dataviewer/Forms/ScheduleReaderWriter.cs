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
using Visu_dataviewer.Bacnet_objects;
namespace Visu_dataviewer
{
    
    public partial class ScheduleReaderWriter : Form
    {
        public ListViewItem selected;

        public ScheduleReaderWriter()
        {
            InitializeComponent();
        }

        public void transferData(ListViewItem selected)
        {
            this.selected = selected;
        }

        public string readSchedule(ListViewItem selected)
        {
            var bacnetDevice = Bac.getBacnetDevice(selected.SubItems[(int)DatapointDefinition.columns.deviceIP].Text, 1);
            var bacnetObject = Bac.getBacnetObject(selected.SubItems[(int)DatapointDefinition.columns.objectType].Text, Convert.ToUInt16(selected.SubItems[(int)DatapointDefinition.columns.objectInstance].Text));
            var type = selected.SubItems[(int)DatapointDefinition.columns.datapointDatatype].Text;
            return Bac.readSchedule(bacnetDevice, bacnetObject, type);
        }

        public void representSchedule(List<List<string>> schedule)
        {
            var isEnum = selected.SubItems[(int)DatapointDefinition.columns.txt01].Text == "" ? false : true;
            foreach (string command in normalizeScheduleEvents(collectScheduleCommands(schedule), isEnum))
            {
                var parsedCommand = command.Split(';');
                dataGridView2.Rows.Add(parsedCommand);
            }
        }

        public List<string> collectPossibleCommands(ListViewItem selected)
        {
            var possibleCommands = new List<string>();
            for (int i = (int)DatapointDefinition.columns.txt00; i < (int)DatapointDefinition.columns.txt15; i++)
            {
                possibleCommands.Add(selected.SubItems[i].Text);
            }
            return possibleCommands;
        }

        public List<string> normalizeScheduleEvents(List<string> commands, bool isEnum)
        {
            var normalizedScheduleEvents = new List<string>();
            foreach (string command in commands)
            {
                var data = command.Split(';');
                var hour = new String('0', 2 - data[0].Length) + data[0];
                var minute = new String('0', 2 - data[1].Length) + data[1];
                var second = new String('0', 2 - data[2].Length) + data[2];
                var millisecond = new String('0', 3 - data[3].Length) + data[3];
                var value = isEnum ? collectPossibleCommands(selected)[int.Parse(data[5])] : data[5];
                normalizedScheduleEvents.Add(hour + " : " + minute + " : " + second + " : " + millisecond + ";" + value);
            }
            return normalizedScheduleEvents;
        }

        public List<string> collectScheduleCommands(List<List<string>> schedule)
        {
            
            var commands = new List<string>();
            foreach(List<string> day in schedule)
            {
                foreach (string command in day)
                {
                    if (!commands.Contains(command))
                        commands.Add(command);
                }
            }
            return commands;
        }

        public List<List<string>> parseSchedule(ListViewItem selected)
        {
            var schedule = readSchedule(selected);
            var parsedSchedule = new List<List<string>>();
            
            var days = schedule.Split(new string[] { "DaySep" }, StringSplitOptions.None);
            foreach (string day in days)
            {
                var commands = day.Split(new string[] { "ProgSep" }, StringSplitOptions.RemoveEmptyEntries);
                var parsedDay = new List<string>();
                foreach (string command in commands)
                {
                    
                    parsedDay.Add(command);
                }
                parsedSchedule.Add(parsedDay.ToList());
            }
            return parsedSchedule;
        }

        public void addHeaders(string type, List<string> possibleCommands)
        {
            var timeColumn = new DataGridViewTextBoxColumn() { Name = "Time", HeaderText = "Time", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells };
            DataGridViewColumn actionColumn;
            if (type == "enum")
            {
                actionColumn = new DataGridViewComboBoxColumn() {Name = "Action", HeaderText = "Action", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DataSource = possibleCommands};
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

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }


    }
}
