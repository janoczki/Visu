using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visu_dataviewer.Schedule
{
    class ScheduleProperty
    {
        public List<string> PossibleCommands { get; set; }
        public string ActionType { get; set; }
        public ScheduleProperty(List<string> possibleCommands, string actionType)
        {
            this.PossibleCommands = possibleCommands;
            this.ActionType = actionType;
        }
    }
}
