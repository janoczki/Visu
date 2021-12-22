using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.BACnet;
//using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Timers;
using System.IO.BACnet.Serialize;
using System.Globalization;

namespace Visu_dataviewer.Workers
{
    class Reader
    {
        public static void DoWork(object sender, DoWorkEventArgs e)
        {
            Log.Append("polling started");
            var table = global.bigDatapointTable;
            foreach (List<string> column in table)
            {
                var poll = !bool.Parse(column[(int)DatapointDefinition.columns.datapointCOV]);
                var bacnetDevice = Bac.getBacnetDevice(column[(int)DatapointDefinition.columns.deviceIP], 1);
                var bacnetObject = Bac.getBacnetObject(column[(int)DatapointDefinition.columns.objectType], Convert.ToUInt16(column[(int)DatapointDefinition.columns.objectInstance]));

                if (poll)
                {
                    var value = Bac.read(bacnetDevice, bacnetObject);
                    Datapoints.record(bacnetDevice, bacnetObject, value);
                }
            }
        }

        public static void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.Append("polling finished");
            sender = null;
            GC.Collect();
        }

    }
}
