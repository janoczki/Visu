//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO.BACnet;
//using System.Data;
//using System.IO.BACnet.Serialize;
//using System.IO.BACnet.Storage;
//using System.Net.Http.Headers;
//using System.Windows.Forms;

//namespace Visu_dataviewer
//{

//    public class ScheduleObject : BacnetObject
//    {
//        public static string Schedule = "";
//        public static bool ScheduleIsInCommand;
//        public static int ScheduleActualBytesToRead = 0;
//        public static bool ScheduleIsInDay;
//        public static byte[] SCH;
//        public static byte[] SchShrinked;
//        public List<List<string>> FinalSchedule;

//        public ScheduleObject(BacnetAddress device, string objectType, uint objectInstance) : base(device, objectType,objectInstance)
//        {
//            this.Device = device;
//            this.ObjectType = objectType;
//            this.ObjectInstance = objectInstance;
//        }


//        public List<List<string>> ReadSchedule()
//        {
//            var days = new List<List<string>>();
//            byte[] InOutBuffer = null;
//            int offset = 1;
//            byte tag_number;
//            uint len_value_type;

//            Bac.BacnetClient.RawEncodedDecodedPropertyConfirmedRequest(Device, Object, BacnetPropertyIds.PROP_WEEKLY_SCHEDULE, BacnetConfirmedServices.SERVICE_CONFIRMED_READ_PROPERTY, ref InOutBuffer);
            
//            for (int i = 1; i < 8; i++)
//            {
//                var isContent = false;
//                var day = new List<string>();
//                var time = "";
//                //var weekdayname = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.ShortestDayNames[i % 7];
                
//                offset += ASN1.decode_tag_number(InOutBuffer, offset, out tag_number);
//                while (!ASN1.IS_CLOSING_TAG(InOutBuffer[offset]))
//                {
//                    isContent = true;
//                    BacnetValue value;
//                    String command;

//                    // Time
//                    offset += ASN1.decode_tag_number_and_value(InOutBuffer, offset, out tag_number, out len_value_type);
//                    offset += parseDate(InOutBuffer, offset, out time);

//                    // Value
//                    offset += ASN1.decode_tag_number_and_value(InOutBuffer, offset, out tag_number, out len_value_type);
//                    offset += ASN1.bacapp_decode_data(InOutBuffer, offset, InOutBuffer.Length, (BacnetApplicationTags)tag_number, len_value_type, out value);

//                    command = value.Tag != BacnetApplicationTags.BACNET_APPLICATION_TAG_NULL ? time + " = " + Property.SerializeValue(value, value.Tag) : time + " = null";
//                    day.Add(command);
//                }
//                if (!isContent) day.Add("");
//                isContent = false;
//                offset++;
//                days.Add(day);
//            }
//            FinalSchedule = days;
//            return days;
//        }

//        public List<string> CollectPossibleCommands(ListViewItem selected)
//        {
//            var possibleCommands = new List<string>();
//            for (int i = (int)DatapointDefinition.Columns.Txt00; i < (int)DatapointDefinition.Columns.Txt15; i++)
//            {
//                    possibleCommands.Add(selected.SubItems[i].Text);
//            }
//            return string.Join("", possibleCommands) != "" ? possibleCommands : null;
//        }

//        public List<string> CollectActions()
//        {
//            var actions = new List<string>();
//            foreach (var day in FinalSchedule)
//            {
//                foreach (var action in day)
//                {
//                    if (actions.Contains(action) || action == "") continue;
//                    actions.Add(action);
//                }
//            }
//            actions.Sort();
//            return actions;
//        }

//        public static int parseDate(byte[] InOutBuffer, int offset, out string value)
//        {
//            var h = new string('0', 2 - InOutBuffer[offset].ToString().Replace("255","**").Length) + InOutBuffer[offset].ToString().Replace("255", "**");
//            var m = new string('0', 2 - InOutBuffer[offset + 1].ToString().Length) + InOutBuffer[offset + 1].ToString();
//            var s = new string('0', 2 - InOutBuffer[offset + 2].ToString().Length) + InOutBuffer[offset + 2].ToString();
//            var hu = new string('0', 2 - InOutBuffer[offset + 3].ToString().Length) + InOutBuffer[offset + 3].ToString();
//            value = h + ":" + m + ":" + s + ":" + hu;
//            return 4;
//        }

//        public void Write()
//        {
//            //byte[] InOutBuffer = new byte[] {62, 14, 15, 14, 15, 14, 15, 14, 15, 14, 15, 14, 15, 14, 15, 63 };
//            var lst = new List<byte>();
//            lst.Add(62);
//            lst.AddRange(collectScheduleToWrite());
//            lst.Add(63);
//            byte[] InOutBuffer = lst.ToArray();
//            Bac.BacnetClient.RawEncodedDecodedPropertyConfirmedRequest(Device, Object, BacnetPropertyIds.PROP_WEEKLY_SCHEDULE, BacnetConfirmedServices.SERVICE_CONFIRMED_WRITE_PROPERTY, ref InOutBuffer);
//        }

//        public List<byte> collectScheduleToWrite()
//        {
//            var days = new List<byte>();
//            for (int i = 0; i < 7; i++)
//            {
//                var day = new List<byte>();
//                day.Add(14);
//                var srw = Application.OpenForms["ScheduleReaderWriter"] as Forms.ScheduleReaderWriter;
//                foreach (DataGridViewRow row in srw.dataGridView2.Rows)
//                {
//                    if ((bool)row.Cells[i + 2].Value)
//                    {
//                        day.AddRange(getTimeToWrite(row.Cells[0]));
//                        //UNDONE var asd = CollectPossibleCommands();
//                        ASN1.encode_bacnet_real(new EncodeBuffer(new byte[4], 0), 5.2f);
//                    }
                        

//                }
//                day.Add(15);
//                days.AddRange(day);
//            }
//            return days;
//        }

//        public byte[] getTimeToWrite(object time)
//        {
//            var timeString = (time as DataGridViewCell).Value.ToString().Replace("**","255");
//            return timeString.Split(':').Select(byte.Parse).ToArray();
//        }
//    }
//}
