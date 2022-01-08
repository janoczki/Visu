using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Visu_dataviewer.BacnetObjects;
namespace Visu_dataviewer.Workers
{
    class Subscriber
    {
        public static void Work(object sender, DoWorkEventArgs e)
        {
            Log.Append("subscription started");
            var table = Datapoints.Table;
            foreach (List<string> column in table)
            {
                var bacnetDevice = Bac.GetBacnetDevice(column[(int)DatapointDefinition.Columns.DeviceIp], 1);
                var bacnetObject = Bac.GetBacnetObject(column[(int)DatapointDefinition.Columns.ObjectType], Convert.ToUInt16(column[(int)DatapointDefinition.Columns.ObjectInstance]));
                var cov = bool.Parse(column[(int)DatapointDefinition.Columns.DatapointCov]);

                if (!cov) continue;
                var obj = new NormalObject(bacnetDevice, bacnetObject);
                obj.Subscribe();
            }
        }

        public static void Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.Append("subscription complete");
        }
    }
}
