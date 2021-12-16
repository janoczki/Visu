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

namespace Visu_dataviewer
{
    public class Bac
    {
        public static string schedule = "";
        public static bool scheduleIsInCommand;
        public static int scheduleActualBytesToRead = 0;
        public static bool scheduleIsInDay;
        public static bool availabilityCheckComplete = false;
        public static List<string> error = new List<string>();
        public static Dictionary<string, BacnetObjectTypes> BacnetObject;
        public static BacnetClient bacnet_client;

        public static bool startActivity(string localEndpoint)
        {
            try
            {
                bacnet_client = new BacnetClient(new BacnetIpUdpProtocolTransport(0xBAC0, false, false, 1472, localEndpoint));
                bacnet_client.WritePriority = 8;
                bacnet_client.OnCOVNotification += new BacnetClient.COVNotificationHandler(handler_OnCOVNotification);
                bacnet_client.Start();
                Log.Append("Bacnet started");
                return true;
            }
            catch (Exception ex)
            {
                Log.Append("Error when starting bacnet :" + ex.Message);
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static BacnetObjectId getBacnetObject(string objectType, uint instance)
        {
            BacnetObjectId bacnetobj = new BacnetObjectId();
            BacnetObject = new Dictionary<string, BacnetObjectTypes>()
            {
                { "AI", BacnetObjectTypes.OBJECT_ANALOG_INPUT },            { "AO", BacnetObjectTypes.OBJECT_ANALOG_OUTPUT },       { "AV", BacnetObjectTypes.OBJECT_ANALOG_VALUE },
                { "BI", BacnetObjectTypes.OBJECT_BINARY_INPUT },            { "BO", BacnetObjectTypes.OBJECT_BINARY_OUTPUT },       { "BV", BacnetObjectTypes.OBJECT_BINARY_VALUE },
                { "MI", BacnetObjectTypes.OBJECT_MULTI_STATE_INPUT },       { "MO", BacnetObjectTypes.OBJECT_MULTI_STATE_OUTPUT },  { "MV", BacnetObjectTypes.OBJECT_MULTI_STATE_VALUE },
                { "PIV", BacnetObjectTypes.OBJECT_POSITIVE_INTEGER_VALUE }, { "SC", BacnetObjectTypes.OBJECT_SCHEDULE },            { "DEV", BacnetObjectTypes.OBJECT_DEVICE }
            };
            BacnetObject.TryGetValue(objectType, out bacnetobj.type);
            bacnetobj.Instance = instance;
            return bacnetobj;
        }

        public static BacnetAddress getBacnetDevice(string ipAddress, ushort networkNumber)
        {
            BacnetAddress bacnetDev = new BacnetAddress(BacnetAddressTypes.IP, ipAddress, networkNumber);
            return bacnetDev;
        }

        public static string decodePresentValue(IList<BacnetValue> NoScalarValue)
        {
            BacnetValue Value;
            Value = NoScalarValue[0];
            return Value.Value.ToString();
        }

        public static string decodeWeeklySchedule(byte[] data)
        {
            // UNDONE: NOT IMPLEMENTED - decodeWeeklySchedule
            return "";
        }

        public static void readAllValue()
        {
            var poller = new BackgroundWorker();
            poller.DoWork += new DoWorkEventHandler(reader_DoWork);
            poller.RunWorkerCompleted += new RunWorkerCompletedEventHandler(reader_RunWorkerCompleted);
            poller.RunWorkerAsync();
        }

        private static void reader_DoWork(object sender, DoWorkEventArgs e)
        {
            Log.Append("polling started");
            var table = _global.bigDatapointTable;
            foreach (List<string> column in table)
            {
                var poll = !bool.Parse(column[(int)DatapointDefinition.columns.datapointCOV]);
                var bacnetDevice = getBacnetDevice(column[(int)DatapointDefinition.columns.deviceIP], 1);
                var bacnetObject = getBacnetObject(column[(int)DatapointDefinition.columns.objectType], Convert.ToUInt16(column[(int)DatapointDefinition.columns.objectInstance]));

                if (poll)
                {
                    var value = Bac.readValue(bacnetDevice, bacnetObject);
                    Datapoints.record(bacnetDevice, bacnetObject, value);
                }
            }
        }

        private static void reader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.Append("polling finished");
            sender = null;
            GC.Collect();
        }

        public static string readValue(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject)
        {
            try
            {
                IList<BacnetValue> Values;
                bacnet_client.ReadPropertyRequest(bacnetDevice, bacnetObject, BacnetPropertyIds.PROP_PRESENT_VALUE, out Values);
                return decodePresentValue(Values);
            }
            catch (Exception ex)
            {
                Log.Append(ex.Message);
            }
            return null;
        }

        public static string readSchedule(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject)
        {
            try
            {
                byte[] byteValues = null;
                bacnet_client.RawEncodedDecodedPropertyConfirmedRequest(bacnetDevice, bacnetObject,
                    BacnetPropertyIds.PROP_WEEKLY_SCHEDULE, BacnetConfirmedServices.SERVICE_CONFIRMED_READ_PROPERTY,
                    ref byteValues);
                return decodeWeeklySchedule(byteValues);
            }
            catch (Exception ex)
            {
                Log.Append(ex.Message);
            }
            return null;
        }

        //public static List<List<string>> preParseWeeklySchedule(byte[] schedule, string type)
        //{
        //    var day = new List<List<string>>();
        //    var counter = 0;
        //    do
        //    {
        //        if (schedule[counter].ToString() == "14")
        //        {
        //            var dayEvents = new List<string>();
        //            var divider =(schedule.Length-16) % 7 == 0 ? 7 : 10;
        //            var dayEventCounter = 0;
        //            while (counter + dayEventCounter < schedule.Length)
        //            {
        //                if (dayEventCounter % divider == 1 & schedule[counter + dayEventCounter].ToString() == "15")
        //                {
        //                    break;
        //                }
        //                dayEvents.Add(schedule[counter + dayEventCounter].ToString());
        //                dayEventCounter++;
        //            }
        //            counter += dayEventCounter;
        //            day.Add(dayEvents);
        //        }
        //        counter++;
        //    } while (schedule[counter].ToString() != "63");
        //    return day;
        //}

        //public static List<List<string>> parseWeeklySchedule(byte[] schedule)
        //{
        //    var preParsed = preParseWeeklySchedule(schedule, "");
        //    var parsed = new List<List<string>>();

        //    foreach (var day in preParsed)
        //    {
        //        var preDayEvent = new List<string>();
        //        var preDayEventString = "";
        //        preDayEventString = String.Join(" ", day);

        //        var realEvent = preDayEventString.Split(new string[] { " 180 " }, StringSplitOptions.None);

        //        preDayEvent = realEvent.ToList();
        //        preDayEvent.RemoveAt(0);

        //        var dayEvent = new List<string>();
        //        foreach (var eventProperty in preDayEvent)
        //        {
        //            var prop = eventProperty.Split(' ');
        //            var timeAndCommand = prop[0] + ":" + prop[1] + ":" + prop[2] + ":" + prop[3] + ":" + prop[5];
        //            dayEvent.Add(timeAndCommand);
        //        }
        //        parsed.Add(dayEvent);
        //    }
        //    return parsed;
        //}

        private static void tryToBeRecursive(byte[] scheduleArray, int counter, int bytesToRead, int type)
        {
            if (counter < scheduleArray.Length)
            {
                var item = scheduleArray[counter];
                var stringToJoin = "";
                switch (item)
                {
                    case 62:
                        break;

                    case 14:
                        scheduleIsInDay = true;
                        break;

                    case 15:
                        stringToJoin = (counter != scheduleArray.Length - 2) ? "DaySep" : "";
                        scheduleIsInCommand = true;
                        scheduleIsInDay = false;
                        break;

                    case 180:
                        scheduleIsInCommand = true;
                        var temp = "";
                        while (scheduleActualBytesToRead != 0)

                        {
                            var actual = scheduleArray[counter + 1 + (bytesToRead - scheduleActualBytesToRead)].ToString();
                            var separator = (scheduleActualBytesToRead != 1) ? ";" : "";
                            temp += actual + separator;

                            scheduleActualBytesToRead--;
                        }

                        if (type == 68)
                        {
                            var array = temp.Split(';');
                            var value = convertToSingle(array);
                            var time = convertToTime(array);
                            stringToJoin += time + value;
                        }
                        else
                        {
                            stringToJoin += temp;
                        }

                        counter += 6;
                        scheduleActualBytesToRead = bytesToRead;
                        stringToJoin += "ProgSep";
                        scheduleIsInCommand = false;
                        break;

                    case 63:
                        stringToJoin = "";
                        break;
                }
                schedule += stringToJoin;
                tryToBeRecursive(scheduleArray, counter + 1, bytesToRead, type);
                //return false;
            }
            // return true;
        }

        public static string callRecursion(byte[] array, int bytesToRead, int type)
        {
            scheduleActualBytesToRead = bytesToRead;
            schedule = "";
            tryToBeRecursive(array, 0, bytesToRead, type);
            return schedule;
        }

        public static void writeSchedule(ushort networkNumber, string deviceIP, uint deviceInstance, string objectType, uint objectInstance, byte[] value)
        {
            var bacnetDevice = getBacnetDevice(deviceIP, networkNumber);
            var bacnetObject = getBacnetObject(objectType, objectInstance);
            var bacnetProperty = BacnetPropertyIds.PROP_WEEKLY_SCHEDULE;

            var InOutBuffer = value;
            //var InOutBuffer = new byte[] {
            //    62,
            //    14,15, //hétfő
            //    14,180,2,2,2,2,145,0,15, //kedd
            //    14,180,3,3,3,3,145,0,180,3,3,3,4,145,1,15, //szerda
            //    14,180,4,4,4,4,145,0,15, //csütörtök
            //    14,180,5,5,5,5,145,0,15, //péntek
            //    14,180,6,6,6,6,145,0,15, //szombat
            //    14,180,7,7,7,7,145,0,15, //vasárnap
            //    63 };

            bacnet_client.RawEncodedDecodedPropertyConfirmedRequest(bacnetDevice, bacnetObject, bacnetProperty, BacnetConfirmedServices.SERVICE_CONFIRMED_WRITE_PROPERTY, ref InOutBuffer);

        }

        public static void writeBoolValue(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject, dynamic valueToBeWritten)
        {
            BacnetValue[] bacnetValueToBeWritten = new BacnetValue[] { new BacnetValue(valueToBeWritten) };
            bacnetValueToBeWritten[0].Value = Convert.ToUInt32(bacnetValueToBeWritten[0].Value);
            bacnetValueToBeWritten[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_ENUMERATED;
            try
            {
                bacnet_client.WritePropertyRequest(bacnetDevice, bacnetObject, BacnetPropertyIds.PROP_PRESENT_VALUE, bacnetValueToBeWritten);
            }
            catch (Exception e)
            {
                var err = (e.Message == "Error from device: ERROR_CLASS_PROPERTY - ERROR_CODE_WRITE_ACCESS_DENIED") ? "Readonly" : e.Message;
                MessageBox.Show(err);
            }
        }

        public static void writeIntValue(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject, dynamic valueToBeWritten)
        {
            BacnetValue[] bacnetValueToBeWritten = new BacnetValue[] { new BacnetValue(valueToBeWritten) };
            bacnetValueToBeWritten[0].Value = Convert.ToUInt32(bacnetValueToBeWritten[0].Value);
            bacnetValueToBeWritten[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_UNSIGNED_INT;
            try
            {
                bacnet_client.WritePropertyRequest(bacnetDevice, bacnetObject, BacnetPropertyIds.PROP_PRESENT_VALUE, bacnetValueToBeWritten);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void writeFloatValue(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject, dynamic valueToBeWritten)
        {
            var bacnetValueToBeWritten = new BacnetValue[] { new BacnetValue(valueToBeWritten)};
            var reset = new BacnetValue[] { new BacnetValue(null) };
            if (valueToBeWritten != "null")
            {
                bacnetValueToBeWritten[0].Value = Convert.ToSingle(bacnetValueToBeWritten[0].Value);
                bacnetValueToBeWritten[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL;
            }
            var realValueToBeWritten = valueToBeWritten == "null" ? reset : bacnetValueToBeWritten;
            try
            {
                bacnet_client.WritePropertyRequest(bacnetDevice, bacnetObject, BacnetPropertyIds.PROP_PRESENT_VALUE, realValueToBeWritten);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void writeValue(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject, dynamic valueToBeWritten, string format)
        {
            switch (format)
            {
                case "binary": writeBoolValue(bacnetDevice, bacnetObject, valueToBeWritten); break;
                case "integer": writeIntValue(bacnetDevice, bacnetObject, valueToBeWritten); break;
                case "float":  writeFloatValue(bacnetDevice, bacnetObject, valueToBeWritten); break;
            }
            Datapoints.record(bacnetDevice, bacnetObject, valueToBeWritten);
        }

        public static BacnetValue[] getBacnetValue(BacnetObjectId bacnetObject, string value, string format, bool reset)
        {
            var bacnetValueArray = new BacnetValue[] {new BacnetValue()};

            if (!reset)
            {
                switch (format)
                {
                    case "binary": bacnetValueArray[0].Value = Convert.ToUInt32(value); bacnetValueArray[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_ENUMERATED; break;
                    case "int": bacnetValueArray[0].Value = Convert.ToUInt32(value); bacnetValueArray[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_UNSIGNED_INT; break;
                    case "float": bacnetValueArray[0].Value = Convert.ToSingle(value); bacnetValueArray[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL; break;
                }
            }
            else
            {
                switch (format)
                {
                    case "binary": bacnetValueArray[0].Value = null; bacnetValueArray[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_ENUMERATED; break;
                    case "int": bacnetValueArray[0].Value = null; bacnetValueArray[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_UNSIGNED_INT; break;
                    case "float": bacnetValueArray[0].Value = null; bacnetValueArray[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL; break;
                }
            }

            return bacnetValueArray;
        }

        public static void lofasz(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject, dynamic value, string format, bool reset)
        {
            var ertek = getBacnetValue(bacnetObject, value, format, reset);
            try
            {
                bacnet_client.WritePropertyRequest(bacnetDevice, bacnetObject, BacnetPropertyIds.PROP_PRESENT_VALUE, ertek);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        #region CoV

        public static void subscribeToAll()
        {
            var subscriber = new BackgroundWorker();
            subscriber.DoWork += new DoWorkEventHandler(subscriber_DoWork);
            subscriber.RunWorkerCompleted += new RunWorkerCompletedEventHandler(subscriber_RunWorkerCompleted);
            subscriber.RunWorkerAsync();
        }

        public static void subscribe(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject)
        {
            try
            {
                bacnet_client.SubscribeCOVRequest(bacnetDevice, bacnetObject, 0, false, false, _global.covLifetime);
            }
            catch (Exception ex)
            {
                var failureReason = ex.ToString();
                Log.Append("Subscription failed " + bacnetDevice.adr + " " + bacnetObject.type + " " + bacnetObject.instance + " Reason: " + failureReason);
            }
        }

        public static void subscriber_DoWork(object sender, DoWorkEventArgs e)
        {
            var table = _global.bigDatapointTable;
            foreach (List<string> column in table)
            {
                var bacnetDevice = getBacnetDevice(column[(int)DatapointDefinition.columns.deviceIP], 1);
                var bacnetObject = getBacnetObject(column[(int)DatapointDefinition.columns.objectType],  Convert.ToUInt16(column[(int)DatapointDefinition.columns.objectInstance]));
                var cov = bool.Parse(column[(int)DatapointDefinition.columns.datapointCOV]);
                
                if (cov)
                {
                    subscribe(bacnetDevice, bacnetObject);
                }
            }
        }

        private static void subscriber_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.Append("subscribe complete");
        }

        public static void handler_OnCOVNotification(BacnetClient sender, BacnetAddress bacnetDevice, byte invoke_id, uint subscriberProcessIdentifier, BacnetObjectId initiatingDeviceIdentifier, BacnetObjectId bacnetObject, uint timeRemaining, bool need_confirm, ICollection<BacnetPropertyValue> values, BacnetMaxSegments max_segments)
        {
            if ((BacnetPropertyIds)values.ToList()[0].property.propertyIdentifier == BacnetPropertyIds.PROP_PRESENT_VALUE)
                Datapoints.record(bacnetDevice, bacnetObject, values.ToList()[0].value[0].ToString());

            if (need_confirm)
                sender.SimpleAckResponse(bacnetDevice, BacnetConfirmedServices.SERVICE_CONFIRMED_COV_NOTIFICATION, invoke_id);
        }
   
        #endregion

        #region format / convert

        public static string customTypeFromBacnetObjectType(BacnetObjectTypes objecttype)
        {
            switch (objecttype)
            {
                case BacnetObjectTypes.OBJECT_ANALOG_INPUT:
                    return "AI";
                case BacnetObjectTypes.OBJECT_ANALOG_OUTPUT:
                    return "AO";
                case BacnetObjectTypes.OBJECT_ANALOG_VALUE:
                    return "AV";
                case BacnetObjectTypes.OBJECT_BINARY_INPUT:
                    return "BI";
                case BacnetObjectTypes.OBJECT_BINARY_OUTPUT:
                    return "BO";
                case BacnetObjectTypes.OBJECT_BINARY_VALUE:
                    return "BV";
                case BacnetObjectTypes.OBJECT_MULTI_STATE_INPUT:
                    return "MI";
                case BacnetObjectTypes.OBJECT_MULTI_STATE_OUTPUT:
                    return "MO";
                case BacnetObjectTypes.OBJECT_MULTI_STATE_VALUE:
                    return "MV";
                case BacnetObjectTypes.OBJECT_POSITIVE_INTEGER_VALUE:
                    return "PIV";
            }

            return "";
        }

        public static string convertToIP(BacnetAddress adr)
        {
            var address = adr.adr;
            Array.Resize(ref address, 4);

            return new System.Net.IPAddress(address).ToString();
        }

        public static BacnetAddress convertToIP2(BacnetAddress adr)
        {

            return (BacnetAddress)adr;
        }

        public static string formatValue(string value, string format)
        {
            var correctValue = "";
            switch (format)

            {
                case "int":
                    correctValue = Convert.ToInt32(double.Parse(value)).ToString();
                    break;
                case "uint":
                    //correctValue = value[0] == '-' ? "0" : Convert.ToUInt32(double.Parse(value)).ToString();
                    correctValue = Convert.ToUInt32(double.Parse(value)).ToString();
                    break;
                case "float":
                    correctValue = Math.Round(double.Parse(value),3).ToString();
                    break;
                case "binary":
                    correctValue = Convert.ToBoolean(double.Parse(value)).ToString();
                    break;
            }

            return correctValue;
        }

        public static string convertToTime(string[] array)
        {
            return array[0] + array[1] + array[2] + array[3];
        }

        public static float convertToSingle(string[] array)
        {
            var var1 = Convert.ToByte(array[8]);
            var var2 = Convert.ToByte(array[7]);
            var var3 = Convert.ToByte(array[6]);
            var var4 = Convert.ToByte(array[5]);
            var tempbytearray = new byte[] { var1, var2, var3, var4 };
            var value = BitConverter.ToSingle(tempbytearray, 0);
            return value;
        }

        #endregion
    }
}
