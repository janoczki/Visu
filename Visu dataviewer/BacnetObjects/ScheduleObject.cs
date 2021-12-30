using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.BACnet;
namespace Visu_dataviewer.Bacnet_objects
{
    public class ScheduleObject
    {
        byte[] response;
        public BacnetAddress DeviceID { get; set; }
        public BacnetObjectId ObjectID { get; set; }

        public ScheduleObject(BacnetAddress deviceID, BacnetObjectId objectID)
        {
            this.ObjectID = objectID;
            this.DeviceID = deviceID;
        }

        public string Read()
        {
            Bac.bacnet_client.RawEncodedDecodedPropertyConfirmedRequest(DeviceID, ObjectID, BacnetPropertyIds.PROP_WEEKLY_SCHEDULE, BacnetConfirmedServices.SERVICE_CONFIRMED_READ_PROPERTY, ref response);
            return string.Join(";", response);
        }

        public void Write(dynamic value)
        {
            //UNDONE: Schedule write
            throw new NotImplementedException("Feature not implemented yet");
        }
    }
}
