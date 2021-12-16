using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.BACnet;

namespace Visu_dataviewer
{
    public static class Datapoints
    {
        public static List<List<string>> table;

        static Datapoints()
        {
            table = new List<List<string>>();
        }

        public static void record(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject, string value)
        {
            foreach (var row in table)
            {
                var actualBacnetDev = Bac.getBacnetDevice(row[(int)DatapointDefinition.columns.deviceIP], 1);
                var actualBacnetObj = Bac.getBacnetObject(row[(int)DatapointDefinition.columns.objectType], Convert.ToUInt16(row[(int)DatapointDefinition.columns.objectInstance]));

                var deviceMatch = actualBacnetDev.Equals(bacnetDevice) ? true : false;
                var objectMatch = actualBacnetObj.Equals(bacnetObject) ? true : false;
                var datapointValue = table[table.IndexOf(row)][(int)DatapointDefinition.columns.value];

                if (deviceMatch & objectMatch)
                {
                    datapointValue = value;
                    var index = table.IndexOf(row);
                    table[table.IndexOf(row)][(int)DatapointDefinition.columns.value] = value;
                    break;
                }


            }
        }
    }
}
