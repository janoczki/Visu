using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Visu_dataviewer.BacnetObjects;
using System.Globalization;
namespace Visu_dataviewer.Forms
{
    
    public partial class ScheduleReaderWriter : Form
    {
        private ListViewItem Selected { get; set; }
        private List<List<string>> Schedule { get; set; }
        private List<string> Actions { get; set; }
        private List<string> PossibleCommands { get; set; }
        private List<string> CommandsForGui { get; set; }
        private string Type { get; set; }
        public ScheduleReaderWriter(ListViewItem selected, List<List<string>> schedule, List<string> actions, List<string> possibleCommands, string type)
        {
            InitializeComponent();
            this.Selected = selected;
            this.Schedule = schedule;
            this.Actions = actions;
            this.PossibleCommands = possibleCommands;
            this.CommandsForGui = getPossibleCommandsForGUI();
            this.Type = type;
            AddColumns();
        }
  
        private void AddColumns()
        {
            var timeColumn = new DataGridViewTextBoxColumn() { Name = "Time", HeaderText = "Time", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells };
            DataGridViewColumn actionColumn;

            if (Type == "enum")
            {
                actionColumn = new DataGridViewComboBoxColumn() {Name = "Action", HeaderText = "Action", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DataSource = getPossibleCommandsForGUI() };
            }
            else
            {
                actionColumn = new DataGridViewTextBoxColumn() {Name = "Action", HeaderText = "Action", AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader };
            }


            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { timeColumn, actionColumn });
            
            for (int i = 1; i < 8; i++)
            {
                var dayofname = CultureInfo.InvariantCulture.DateTimeFormat.ShortestDayNames[i % 7];
                dataGridView2.Columns.Add(new DataGridViewCheckBoxColumn() {
                    Name = dayofname,
                    HeaderText = dayofname,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader });
            }
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private List<string> getPossibleCommandsForGUI()
        {
            var commandsForGui = new List<string>();
            if (PossibleCommands == null) return null;
            foreach (var command in PossibleCommands)
            {
                if (command == "") continue;
                commandsForGui.Add(command);
            }
            return commandsForGui;
        }

        private void LoadFormElements()
        {
            var list_0_23 = Enumerable.Range(0, 24).Select(n => new string('0', 2 - n.ToString().Length) + n.ToString()).ToList(); list_0_23.AddRange(new List<string> { "**" });
            var list_0_59 = Enumerable.Range(0, 60).Select(n => new string('0', 2 - n.ToString().Length) + n.ToString()).ToList(); list_0_59.AddRange(new List<string> { "**" });
            var list_0_99 = Enumerable.Range(0, 100).Select(n => new string('0', 2 - n.ToString().Length) + n.ToString()).ToList(); list_0_99.AddRange(new List<string> { "**" });
            comboBox1.DataSource = list_0_23;
            comboBox2.DataSource = comboBox3.DataSource = list_0_59;
            comboBox4.DataSource = list_0_99;

            numericUpDown1.Enabled = Type != "enum";
            comboBox5.Enabled = Type == "enum";
            if (comboBox5.Enabled) comboBox5.DataSource = getPossibleCommandsForGUI();

        }
        private void ScheduleReaderWriter_Load(object sender, EventArgs e)
        {
            LoadFormElements();
            foreach (var command in Actions)
            {
                if (command == "") continue;
                var whichDay = new List<bool>();
                foreach (var day in Schedule)
                {
                    whichDay.Add(day.Contains(command));
                }
                var time = command.Split(new string[] { " = " }, StringSplitOptions.None)[0];
                var val = command.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                var row = new dynamic[] {
                    time, PossibleCommands!=null ? PossibleCommands[int.Parse(val)] : val.ToString(),
                    whichDay[0], whichDay[1], whichDay[2], whichDay[3], whichDay[4], whichDay[5], whichDay[6]};
                dataGridView2.Rows.Add(row);
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var val = comboBox5.Enabled == true ? comboBox5.Text : numericUpDown1.Value.ToString();
            var time = comboBox1.Text + ":" + comboBox2.Text + ":" + comboBox3.Text + ":" + comboBox4.Text;
            var whichDay = new List<bool> {checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, checkBox4.Checked, checkBox5.Checked, checkBox6.Checked, checkBox7.Checked };
            var row = new dynamic[] {
                    time, PossibleCommands!=null ? PossibleCommands[int.Parse(val)] : val.ToString(),
                    whichDay[0], whichDay[1], whichDay[2], whichDay[3], whichDay[4], whichDay[5], whichDay[6]};
            dataGridView2.Rows.Add(row);
            dataGridView2.Sort(dataGridView2.Columns["Time"], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count>0)
            {
                dataGridView2.Rows.RemoveAt(dataGridView2.SelectedCells[0].RowIndex);
            }
        }
    }
}
