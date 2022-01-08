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
using Visu_dataviewer.BacnetObjects;

namespace Visu_dataviewer.Workers
{
    class Reader
    {
        public static void Work(object sender, DoWorkEventArgs e)
        {
            Log.Append("polling started");
            var table = Datapoints.Table;
            foreach (List<string> column in table)
            {
                var poll = !bool.Parse(column[(int)DatapointDefinition.Columns.DatapointCov]);
                var bacnetDevice = Bac.GetBacnetDevice(column[(int)DatapointDefinition.Columns.DeviceIp], 1);
                var bacnetObject = Bac.GetBacnetObject(column[(int)DatapointDefinition.Columns.ObjectType], Convert.ToUInt16(column[(int)DatapointDefinition.Columns.ObjectInstance]));

                if (!poll) continue;
                var obj = new NormalObject(bacnetDevice, bacnetObject);
                var value = obj.Read();
                Datapoints.Record(bacnetDevice, bacnetObject, value);
            }
        }

        public static void Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (sender == null) throw new ArgumentNullException("sender");
            Log.Append("polling finished");
        }

    }
}
