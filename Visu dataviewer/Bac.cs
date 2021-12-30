using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.BACnet;
using System.Windows.Forms;
using System.ComponentModel;

namespace Visu_dataviewer
{
    public class Bac
    {
        public static string schedule = "";
        public static bool scheduleIsInCommand;
        public static int scheduleActualBytesToRead = 0;
        public static bool scheduleIsInDay;
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
        
        public static void readAll()
        {
            var reader = new BackgroundWorker();
            reader.DoWork += new DoWorkEventHandler(Workers.Reader.Work);
            reader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Workers.Reader.Complete);
            reader.RunWorkerAsync();
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

        public static void subscribeToAll()
        {
            var subscriber = new BackgroundWorker();
            subscriber.DoWork += new DoWorkEventHandler(Workers.Subscriber.Work);
            subscriber.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Workers.Subscriber.Complete);
            subscriber.RunWorkerAsync();
        }

        public static void subscribe(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject)
        {
            try
            {
                bacnet_client.SubscribeCOVRequest(bacnetDevice, bacnetObject, 0, false, false, global.covLifetime);
            }
            catch (Exception ex)
            {
                var failureReason = ex.ToString();
                Log.Append("Subscription failed " + bacnetDevice.adr + " " + bacnetObject.type + " " + bacnetObject.instance + " Reason: " + failureReason);
            }
        }

        public static void handler_OnCOVNotification(BacnetClient sender, BacnetAddress bacnetDevice, byte invoke_id, uint subscriberProcessIdentifier, BacnetObjectId initiatingDeviceIdentifier, BacnetObjectId bacnetObject, uint timeRemaining, bool need_confirm, ICollection<BacnetPropertyValue> values, BacnetMaxSegments max_segments)
        {
            if ((BacnetPropertyIds)values.ToList()[0].property.propertyIdentifier == BacnetPropertyIds.PROP_PRESENT_VALUE)
                Datapoints.record(bacnetDevice, bacnetObject, values.ToList()[0].value[0].ToString());

            if (need_confirm)
                sender.SimpleAckResponse(bacnetDevice, BacnetConfirmedServices.SERVICE_CONFIRMED_COV_NOTIFICATION, invoke_id);
        }
        
        public static string convertToTime(string[] array)
        {
            var hour = array[0]; 
            return array[0] + ";" + array[1] + ";" + array[2] + ";" + array[3];
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

                        var array = temp.Split(';');
                        var time = convertToTime(array);
                        var value = "";
                        if (type == 68)
                        {
                            value = convertToSingle(array).ToString();
                            //stringToJoin += time + ";" + type.ToString() + ";" + value;
                        }
                        else
                        {
                            value = array[5];
                            //stringToJoin += time + ";" + type.ToString() + ";" + value;
                            //stringToJoin += temp;
                        }
                        stringToJoin += time + ";" + type.ToString() + ";" + value;
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
            }
        }

        public static string callRecursion(byte[] array, int bytesToRead, int type)
        {
            scheduleActualBytesToRead = bytesToRead;
            schedule = "";
            tryToBeRecursive(array, 0, bytesToRead, type);
            return schedule;
        }

        public static string readSchedule(BacnetAddress bacnetDevice, BacnetObjectId bacnetObject, string type)
        {
            try
            {
                byte[] byteValues = null;
                bacnet_client.RawEncodedDecodedPropertyConfirmedRequest(bacnetDevice, bacnetObject, BacnetPropertyIds.PROP_WEEKLY_SCHEDULE, BacnetConfirmedServices.SERVICE_CONFIRMED_READ_PROPERTY, ref byteValues);
                return callRecursion(byteValues, getScheduleBytesToRead(type), getScheduleType(type));
            }
            catch (Exception ex)
            {
                Log.Append(ex.Message);
                return null;
            }
        }

        public static int getScheduleType(string typeString)
        {
            int type = 0;
            switch (typeString)
            {
                case "float": type = 68; break;
                case "binary": type = 145; break;
                case "int": type = 33; break;
            }
            return type;
        }

        public static int getScheduleBytesToRead(string typeString)
        {
            int bytesToRead = 0;
            switch (typeString)
            {
                case "float": bytesToRead = 10; break;
                default: bytesToRead = 6; break;
            }
            return bytesToRead;
        }
    }
}
