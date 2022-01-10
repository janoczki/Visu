using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visu_dataviewer.Schedule
{
    class ScheduleAction
    {
        public string Time { get; set; }
        public string Command { get; set; }

        public ScheduleAction(string time, string command)
        {
            this.Time = time;
            this.Command = command;
        }
    }
}
