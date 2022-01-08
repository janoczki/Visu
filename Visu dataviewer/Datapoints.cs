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
        public static List<List<string>> Table;
        
        static Datapoints()
        {
            Table = new List<List<string>>();
        }

        public static void Record(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject, string value)
        {
            foreach (var row in Table)
            {
                var actualBacnetDev = Bac.GetBacnetDevice(row[(int)DatapointDefinition.Columns.DeviceIp], 1);
                var actualBacnetObj = Bac.GetBacnetObject(row[(int)DatapointDefinition.Columns.ObjectType], 
                    Convert.ToUInt16(row[(int)DatapointDefinition.Columns.ObjectInstance]));

                if (!(actualBacnetDev.Equals(bacnetDevice) & actualBacnetObj.Equals(bacnetObject))) continue;
                Table[Table.IndexOf(row)][(int)DatapointDefinition.Columns.Value] = value;
                break;
            }
        }
    }
}
