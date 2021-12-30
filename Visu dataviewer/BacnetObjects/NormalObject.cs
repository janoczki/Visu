using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.BACnet;
namespace Visu_dataviewer.BacnetObjects
{
    public class NormalObject
    {
        public BacnetAddress DeviceID { get; set; }
        public BacnetObjectId ObjectID { get; set; }
        public IList<BacnetValue> response;

        public NormalObject(BacnetAddress deviceID, BacnetObjectId objectID)
        {
            this.ObjectID = objectID;
            this.DeviceID = deviceID;
        }

        public string Read()
        {
            Bac.bacnet_client.ReadPropertyRequest(DeviceID, ObjectID, 
                BacnetPropertyIds.PROP_PRESENT_VALUE, out response);
            return response[0].Value != null ? response[0].Value.ToString() : "null";
        }

        public void Write(dynamic value, string format, bool reset)
        {
            Bac.bacnet_client.WritePropertyRequest(
                DeviceID, ObjectID, 
                BacnetPropertyIds.PROP_PRESENT_VALUE,
                Bac.getBacnetValue(ObjectID, value, format, reset));
            Datapoints.record(DeviceID, ObjectID, value);
        }
    }
}
