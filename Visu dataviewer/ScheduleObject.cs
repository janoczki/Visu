using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.BACnet;
using System.IO.BACnet.Serialize;
using System.IO.BACnet.Storage;
namespace Visu_dataviewer
{
    class ScheduleObject
    {
        public BacnetAddress BacnetDevice { get; set; }
        public BacnetObjectId BacnetObject { get; set; }

        public ScheduleObject(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject)
        {
            this.BacnetDevice = bacnetDevice;
            this.BacnetObject = bacnetObject;
        }

        private byte[] Read()
        {
            byte[] InOutBuffer = null;
            Bac.BacnetClient.RawEncodedDecodedPropertyConfirmedRequest(
                BacnetDevice, 
                BacnetObject, 
                BacnetPropertyIds.PROP_WEEKLY_SCHEDULE, 
                BacnetConfirmedServices.SERVICE_CONFIRMED_READ_PROPERTY, 
                ref InOutBuffer);
            return InOutBuffer;
        }

        public List<List<string>> GetSchedule()
        {
            var InOutBuffer = Read();
            var days = new List<List<string>>();
            byte tag_number;
            int offset = 1;
            uint len_value_type;

            for (int i = 1; i < 8; i++)
            {
                var isContent = false;
                var day = new List<string>();
                var time = "";

                offset += ASN1.decode_tag_number(InOutBuffer, offset, out tag_number);
                while (!ASN1.IS_CLOSING_TAG(InOutBuffer[offset]))
                {
                    isContent = true;
                    BacnetValue value;
                    String command;

                    // Time
                    offset += ASN1.decode_tag_number_and_value(InOutBuffer, offset, out tag_number, out len_value_type);
                    offset += parseDate(InOutBuffer, offset, out time);

                    // Value
                    offset += ASN1.decode_tag_number_and_value(InOutBuffer, offset, out tag_number, out len_value_type);
                    offset += ASN1.bacapp_decode_data(InOutBuffer, offset, InOutBuffer.Length, (BacnetApplicationTags)tag_number, len_value_type, out value);

                    command = value.Tag != BacnetApplicationTags.BACNET_APPLICATION_TAG_NULL ? time + " = " + Property.SerializeValue(value, value.Tag) : time + " = null";
                    day.Add(command);
                }
                if (!isContent) day.Add("");
                isContent = false;
                offset++;
                days.Add(day);
            }
            return days;
        }
 
        private static int parseDate(byte[] InOutBuffer, int offset, out string value)
        {
            var h = new string('0', 2 - InOutBuffer[offset].ToString().Replace("255", "**").Length) + InOutBuffer[offset].ToString().Replace("255", "**");
            var m = new string('0', 2 - InOutBuffer[offset + 1].ToString().Length) + InOutBuffer[offset + 1].ToString();
            var s = new string('0', 2 - InOutBuffer[offset + 2].ToString().Length) + InOutBuffer[offset + 2].ToString();
            var hu = new string('0', 2 - InOutBuffer[offset + 3].ToString().Length) + InOutBuffer[offset + 3].ToString();
            value = h + ":" + m + ":" + s + ":" + hu;
            return 4;
        }

        public List<string> GetScheduleCommands(List<List<string>> schedule)
        {
            var actions = new List<string>();
            foreach (var day in schedule)
            {
                foreach (var action in day)
                {
                    if (actions.Contains(action) || action == "") continue;
                    actions.Add(action);
                }
            }
            actions.Sort();
            return actions;
        }
    }
}
