using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace Visu_dataviewer.Workers
{
    class Subscriber
    {
        public static void DoWork(object sender, DoWorkEventArgs e)
        {
            var table = global.bigDatapointTable;
            foreach (List<string> column in table)
            {
                var bacnetDevice = Bac.getBacnetDevice(column[(int)DatapointDefinition.columns.deviceIP], 1);
                var bacnetObject = Bac.getBacnetObject(column[(int)DatapointDefinition.columns.objectType], Convert.ToUInt16(column[(int)DatapointDefinition.columns.objectInstance]));
                var cov = bool.Parse(column[(int)DatapointDefinition.columns.datapointCOV]);

                if (cov)
                {
                    Bac.subscribe(bacnetDevice, bacnetObject);
                }
            }
        }

        public static void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.Append("subscribe complete");
        }
    }
}
