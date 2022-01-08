using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.BACnet;
using System.Data;
using System.IO.BACnet.Serialize;
using System.IO.BACnet.Storage;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace Visu_dataviewer.BacnetObjects
{
    public class ScheduleObject
    {
        //byte[] response;
        public BacnetAddress DeviceId { get; set; }
        public BacnetObjectId ObjectId { get; set; }
        public string Type { get; set; }
        public static string Schedule = "";
        public static bool ScheduleIsInCommand;
        public static int ScheduleActualBytesToRead = 0;
        public static bool ScheduleIsInDay;
        public static byte[] SCH;
        public static byte[] SchShrinked;
        public List<List<string>> FinalSchedule;
        public ScheduleObject(BacnetAddress deviceId, BacnetObjectId objectId)
        {
            this.ObjectId = objectId;
            this.DeviceId = deviceId;
        }



        public List<List<string>> Read()
        {
            var days = new List<List<string>>();
            byte[] InOutBuffer = null;
            int offset = 1;
            byte tag_number;
            uint len_value_type;

            Bac.BacnetClient.RawEncodedDecodedPropertyConfirmedRequest(DeviceId, ObjectId, BacnetPropertyIds.PROP_WEEKLY_SCHEDULE, BacnetConfirmedServices.SERVICE_CONFIRMED_READ_PROPERTY, ref InOutBuffer);
            
            for (int i = 1; i < 8; i++)
            {
                var isContent = false;
                var day = new List<string>();
                var time = "";
                //var weekdayname = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.ShortestDayNames[i % 7];
                
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
            FinalSchedule = days;
            return days;
        }

        public List<string> CollectPossibleCommands(ListViewItem selected)
        {
            var possibleCommands = new List<string>();
            for (int i = (int)DatapointDefinition.Columns.Txt00; i < (int)DatapointDefinition.Columns.Txt15; i++)
            {
                    possibleCommands.Add(selected.SubItems[i].Text);
            }
            return string.Join("", possibleCommands) != "" ? possibleCommands : null;
        }

        public List<string> CollectActions()
        {
            var actions = new List<string>();
            foreach (var day in FinalSchedule)
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

        public static int parseDate(byte[] InOutBuffer, int offset, out string value)
        {
            var h = new string('0', 2 - InOutBuffer[offset].ToString().Replace("255","**").Length) + InOutBuffer[offset].ToString().Replace("255", "**");
            var m = new string('0', 2 - InOutBuffer[offset + 1].ToString().Length) + InOutBuffer[offset + 1].ToString();
            var s = new string('0', 2 - InOutBuffer[offset + 2].ToString().Length) + InOutBuffer[offset + 2].ToString();
            var hu = new string('0', 2 - InOutBuffer[offset + 3].ToString().Length) + InOutBuffer[offset + 3].ToString();
            value = h + ":" + m + ":" + s + ":" + hu;
            return 4;
        }

        public void Write(dynamic value)
        {
            //UNDONE: Schedule write
            throw new NotImplementedException("Feature not implemented yet");
        }

        private static string ConvertToTime(string[] array)
        {
            return array[0] + ";" + array[1] + ";" + array[2] + ";" + array[3];
        }

        private static float ConvertToSingle(string[] array)
        {
            var var1 = Convert.ToByte(array[8]);
            var var2 = Convert.ToByte(array[7]);
            var var3 = Convert.ToByte(array[6]);
            var var4 = Convert.ToByte(array[5]);
            var tempbytearray = new byte[] { var1, var2, var3, var4 };
            var value = BitConverter.ToSingle(tempbytearray, 0);
            return value;
        }

        private static void TryToBeRecursive(byte[] scheduleArray, int counter, int bytesToRead, int type)
        {
            if (counter >= scheduleArray.Length) return;

            var item = scheduleArray[counter];
            var stringToJoin = "";
            switch (item)
            {
                case 62: break;

                case 14:
                    ScheduleIsInDay = true;
                    break;

                case 15:
                    stringToJoin = (counter != scheduleArray.Length - 2) ? "DaySep" : "";
                    ScheduleIsInCommand = true;
                    ScheduleIsInDay = false;
                    break;

                case 180:
                    ScheduleIsInCommand = true;
                    var temp = "";
                    while (ScheduleActualBytesToRead != 0)

                    {
                        var actual = scheduleArray[counter + 1 + (bytesToRead - ScheduleActualBytesToRead)].ToString();
                        var separator = (ScheduleActualBytesToRead != 1) ? ";" : "";
                        temp += actual + separator;

                        ScheduleActualBytesToRead--;
                    }

                    var array = temp.Split(';');
                    var time = ConvertToTime(array);
                    var value = "";
                    value = type == 68 ? ConvertToSingle(array).ToString() : array[5];
                    stringToJoin += time + ";" + type + ";" + value;
                    counter += 6;
                    ScheduleActualBytesToRead = bytesToRead;
                    stringToJoin += "ProgSep";
                    ScheduleIsInCommand = false;
                    break;

                case 63:
                    stringToJoin = "";
                    break;
            }
            Schedule += stringToJoin;
            TryToBeRecursive(scheduleArray, counter + 1, bytesToRead, type);
        }

        public static string CallRecursion(byte[] array, int bytesToRead, int type)
        {
            ScheduleActualBytesToRead = bytesToRead;
            Schedule = "";
            TryToBeRecursive(array, 0, bytesToRead, type);
            return Schedule;
        }

        public static string ReadSchedule(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject, string type)
        {
            try
            {
                byte[] byteValues = null;
                Bac.BacnetClient.RawEncodedDecodedPropertyConfirmedRequest(bacnetDevice, bacnetObject, BacnetPropertyIds.PROP_WEEKLY_SCHEDULE, BacnetConfirmedServices.SERVICE_CONFIRMED_READ_PROPERTY, ref byteValues);
                return CallRecursion(byteValues, GetScheduleBytesToRead(type), GetScheduleType(type));
            }
            catch (Exception ex)
            {
                Log.Append(ex.Message);
                return null;
            }
        }

        private static int GetScheduleType(string type)
        {
            switch (type)
            {
                case "float": return (int)ScheduleType.Float;
                case "binary": return (int)ScheduleType.Binary;
                case "int": return (int)ScheduleType.Int;
            }
            return 0;
        }

        public enum ScheduleType
        {
            Float = 68,
            Binary = 145,
            Int = 33
        }

        private static int GetScheduleBytesToRead(string type)
        {
            return type == "float" ? 12 : 8;
            //switch (type)
            //{
            //    case "float": return 10;
            //    default: return 6;
            //}
        }
    }
}
