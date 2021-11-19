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

        public static BacnetClient bacnet_client;

        public static BacnetPropertyIds getPropertyId(string prop)

        {
            BacnetPropertyIds result;

            switch (prop)
            {
                case "PV":
                    result = BacnetPropertyIds.PROP_PRESENT_VALUE;
                    break;
                case "List":
                    result = BacnetPropertyIds.PROP_OBJECT_LIST;
                    break;
                case "Name":
                    result = BacnetPropertyIds.PROP_OBJECT_NAME;
                    break;
                case "Weekly":
                    result = BacnetPropertyIds.PROP_WEEKLY_SCHEDULE;
                    break;
                default:
                    result = BacnetPropertyIds.PROP_PRESENT_VALUE;
                    break;
            }
            return result;
        }

        public static BacnetObjectId bacnetNode(string objectType, uint instance)
        {
            BacnetObjectId bacnetobj = new BacnetObjectId();
            switch (objectType)
            {
                case "AI":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_ANALOG_INPUT;
                    break;
                case "AO":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_ANALOG_OUTPUT;
                    break;
                case "AV":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_ANALOG_VALUE;
                    break;
                case "BI":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_BINARY_INPUT;
                    break;
                case "BO":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_BINARY_OUTPUT;
                    break;
                case "BV":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_BINARY_VALUE;
                    break;
                case "MI":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_MULTI_STATE_INPUT;
                    break;
                case "MO":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_MULTI_STATE_OUTPUT;
                    break;
                case "MV":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_MULTI_STATE_VALUE;
                    break;
                case "DEV":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_DEVICE;
                    break;
                case "SC":
                    bacnetobj.type = BacnetObjectTypes.OBJECT_SCHEDULE;
                    break;
            }

            bacnetobj.Instance = instance;

            return bacnetobj;
        }

        public static BacnetAddress bacnetDevice(string ipAddress, ushort networkNumber)
        {
            BacnetAddress bacnetDev = new BacnetAddress(BacnetAddressTypes.IP, ipAddress, networkNumber);
            return bacnetDev;
        }

        public static bool startActivity(string localEndpoint)
        {
            try
            {
                bacnet_client = new BacnetClient(new BacnetIpUdpProtocolTransport(0xBAC0, false, false, 1472, localEndpoint));
                bacnet_client.OnCOVNotification += new BacnetClient.COVNotificationHandler(handler_OnCOVNotification);
                bacnet_client.Start();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        #region read



        public static List<List<string>> parseWeeklySchedule(byte[] schedule)
        {
            var preParsed = preParseWeeklySchedule(schedule);
            var parsed = new List<List<string>>();

            foreach (var day in preParsed)
            {
                var preDayEvent = new List<string>();
                var preDayEventString = "";
                preDayEventString = String.Join(" ", day);

                var realEvent = preDayEventString.Split(new string[] { " 180 " }, StringSplitOptions.None);

                preDayEvent = realEvent.ToList();
                preDayEvent.RemoveAt(0);

                var dayEvent = new List<string>();
                foreach (var eventProperty in preDayEvent)
                {
                    var prop = eventProperty.Split(' ');
                    var timeAndCommand = prop[0] + ":" + prop[1] + ":" + prop[2] + ":" + prop[3] + ":" + prop[5];
                    dayEvent.Add(timeAndCommand);
                }
                parsed.Add(dayEvent);
            }
            return parsed;
        }

        public static List<List<string>> preParseWeeklySchedule(byte[] schedule)
        {
            var day = new List<List<string>>();
            var counter = 0;
            do
            {
                if (schedule[counter].ToString() == "14")
                {
                    var dayEvents = new List<string>();

                    var dayEventCounter = 0;
                    while (counter + dayEventCounter < schedule.Length)
                    {
                        if (dayEventCounter % 7 == 1 & schedule[counter + dayEventCounter].ToString() == "15")
                        {
                            break;
                        }
                        dayEvents.Add(schedule[counter + dayEventCounter].ToString());
                        dayEventCounter++;
                    }
                    counter += dayEventCounter;
                    day.Add(dayEvents);
                }
                counter++;
            } while (schedule[counter].ToString() != "63");
            return day;
        }
 
        public static string parseBacnetValue(IList<BacnetValue> NoScalarValue)
        {
            BacnetValue Value;
            Value = NoScalarValue[0];
            return Value.Value.ToString();
        }

        public static void writeSchedule(ushort networkNumber, string deviceIP, uint deviceInstance, string objectType, uint objectInstance, string property)
        {
            var dev = bacnetDevice(deviceIP, networkNumber);
            var node = bacnetNode(objectType, objectInstance);
            var id = getPropertyId(property);

            var InOutBuffer = new byte[] {
                62,
                14,15, //hétfő
                14,180,2,2,2,2,145,0,15, //kedd
                14,180,3,3,3,3,145,0,180,3,3,3,4,145,1,15, //szerda
                14,180,4,4,4,4,145,0,15, //csütörtök
                14,180,5,5,5,5,145,0,15, //péntek
                14,180,6,6,6,6,145,0,15, //szombat
                14,180,7,7,7,7,145,0,15, //vasárnap
                63 };

            bacnet_client.RawEncodedDecodedPropertyConfirmedRequest(dev, node, id, BacnetConfirmedServices.SERVICE_CONFIRMED_WRITE_PROPERTY, ref InOutBuffer);


            //bacnet_client.WritePropertyRequest(
            //    dev,
            //    node,
            //    id,
            //    NoScalarValue);
        }

        public static string readValue(ushort networkNumber, string deviceIP, uint deviceInstance, string objectType, uint objectInstance, string property)
        {
            var dev = bacnetDevice(deviceIP, networkNumber);
            var node = bacnetNode(objectType, objectInstance);
            var id = getPropertyId(property);

                switch (property)
                {
                    case "PV":
                    IList<BacnetValue> NoScalarValue;
                    bacnet_client.ReadPropertyRequest(dev, node, id, out NoScalarValue);

                    var presentValue = parseBacnetValue(NoScalarValue);
                    return presentValue;

                    case "Weekly":
                    byte[] InOutBuffer = null;

                    bacnet_client.RawEncodedDecodedPropertyConfirmedRequest(dev, node, id, BacnetConfirmedServices.SERVICE_CONFIRMED_READ_PROPERTY,  ref InOutBuffer);

                    var weeklySchedule = parseWeeklySchedule(InOutBuffer);

                    return weeklySchedule.ToString();

                    default:
                        return "";
                }
        }

        public static void poll()
        {
            var poller = new BackgroundWorker();
            poller.DoWork += new DoWorkEventHandler(poller_DoWork);
            poller.RunWorkerCompleted += new RunWorkerCompletedEventHandler(poller_RunWorkerCompleted);
            poller.RunWorkerAsync();
        }

        private static void poller_DoWork(object sender, DoWorkEventArgs e)
        {
            readData(_global.bigDatapointTable);
        }

        private static void poller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.append("poll complete");
            sender = null;
            GC.Collect();
        }

        private static void readData(List<List<string>> dataTable)
        {
            foreach (List<string> property in dataTable)
            {
                var cov = property[4];
                var devIP = property[5];
                var devInst = Convert.ToUInt16(property[6]);
                var objType = property[7];
                var objInst = Convert.ToUInt16(property[8]);

                if (bool.Parse(cov) == false)
                {
                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    dataTable[dataTable.IndexOf(property)][9] = Bac.readValue(1, devIP, devInst, objType, objInst, "PV");
                    _global.dataTransfer.Add(new List<string> { property[0], property[9], timestamp });
                }
            }
        }

        public static bool writeValue(ushort networkNumber, string deviceIP, string deviceInstance, string objectType, uint objectInstance, Nullable<int> valueToBeWritten)
        {
            bool ret;
            bacnet_client.WritePriority = 8;
            BacnetValue[] bacnetValueToBeWritten = new BacnetValue[] { new BacnetValue(valueToBeWritten) };

            switch (objectType.Substring(0, 1))
            {
                case "A":
                    bacnetValueToBeWritten[0].Value = Convert.ToSingle(bacnetValueToBeWritten[0].Value);
                    bacnetValueToBeWritten[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL;
                    break;
                case "B":
                    bacnetValueToBeWritten[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_ENUMERATED;
                    break;

                case "M":
                    bacnetValueToBeWritten[0].Value = Convert.ToUInt32(bacnetValueToBeWritten[0].Value);
                    bacnetValueToBeWritten[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_UNSIGNED_INT;
                    break;
            }

            try
            {
                ret = bacnet_client.WritePropertyRequest(
                    bacnetDevice(deviceIP, networkNumber),
                    bacnetNode(objectType, objectInstance),
                    BacnetPropertyIds.PROP_PRESENT_VALUE,
                    bacnetValueToBeWritten);
                return ret;
            }
            catch (Exception e)
            {
                string read = "Error from device: ERROR_CLASS_PROPERTY - ERROR_CODE_WRITE_ACCESS_DENIED";

                if (e.Message == read)
                {
                    MessageBox.Show("Ez az adatpont nem írható");
                }
                else
                {
                    MessageBox.Show(e.Message);
                }
                return false;
            }
        }

        #endregion

        #region CoV

        public static void subscribe()
        {
            var subscriber = new BackgroundWorker();
            subscriber.DoWork += new DoWorkEventHandler(subscriber_DoWork);
            subscriber.RunWorkerCompleted += new RunWorkerCompletedEventHandler(subscriber_RunWorkerCompleted);
            subscriber.RunWorkerAsync();
        }

        public static void subscriber_DoWork(object sender, DoWorkEventArgs e)
        {
            //subscribe(_global.bigDatapointTable);

            foreach (List<string> property in _global.bigDatapointTable)
            {
                var covSubscriptionRequired = bool.Parse(property[4]);
                if (covSubscriptionRequired)
                {
                    var devIP = property[5];
                    var devInst = Convert.ToUInt16(property[6]);
                    var objType = property[7];
                    var objInst = Convert.ToUInt16(property[8]);
                    //Bac.SubscribeToCoV(1, devIP, devInst, objType, objInst, _global.covLifetime);
                    bacnet_client.SubscribeCOVRequest(
                        bacnetDevice(devIP, 1),
                        bacnetNode(objType, objInst),
                        0, false, false, _global.covLifetime);
                }
            }

        }

        private static void subscriber_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.append("subscribe complete");
        }

        public static void handler_OnCOVNotification(BacnetClient sender, BacnetAddress adr, byte invoke_id, uint subscriberProcessIdentifier, BacnetObjectId initiatingDeviceIdentifier, BacnetObjectId monitoredObjectIdentifier, uint timeRemaining, bool need_confirm, ICollection<BacnetPropertyValue> values, BacnetMaxSegments max_segments)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var ip = convertToIP(adr);
            var devInst = initiatingDeviceIdentifier.Instance;
            var objInstance = monitoredObjectIdentifier.Instance;
            var objType = customTypeFromBacnetObjectType(monitoredObjectIdentifier.Type);
            var remainingTime = timeRemaining;


            foreach (BacnetPropertyValue value in values)
            {

                switch ((BacnetPropertyIds)value.property.propertyIdentifier)
                {
                    case BacnetPropertyIds.PROP_PRESENT_VALUE:
                        var rawData = value.value[0].ToString();

                        foreach (var item in _global.bigDatapointTable)
                        {
                            if (
                                (item[5] == ip) &
                                (item[6] == devInst.ToString()) &
                                (item[7] == objType) &
                                (item[8] == objInstance.ToString())
                                )
                            {
                                item[9] = formatValue(rawData, item[2]);
                                if (bool.Parse(item[3]))
                                {

                                    _global.dataTransfer.Add(new List<string> { item[0], item[9], timestamp });
                                }
                                break;
                            }
                        }
                        //WriteFile("SavedList_COV.txt", timestamp + " " + Event + " " + initiatingDeviceIdentifier.ToString() + " " + monitoredObjectIdentifier.ToString() + " " + Value, 10, 10);
                        //logToListbox(timestamp + ": " + Event + " " + initiatingDeviceIdentifier + " " + monitoredObjectIdentifier + " " + Value);
                        break;

                    //case BacnetPropertyIds.PROP_STATUS_FLAGS:
                    //    Event = "Állapotváltozás";
                    //    string status_text = "";
                    //    if (value.value != null && value.value.Count > 0)
                    //    {
                    //        // egyszerre kapom meg a statusflageket, így egyszerre kell visszaadnom az állapotokat
                    //        BacnetStatusFlags status = (BacnetStatusFlags)((BacnetBitString)value.value[0].Value).ConvertToInt();
                    //        if ((status & BacnetStatusFlags.STATUS_FLAG_FAULT) == BacnetStatusFlags.STATUS_FLAG_FAULT)
                    //            status_text = "Hibás érték";
                    //        else if ((status & BacnetStatusFlags.STATUS_FLAG_IN_ALARM) == BacnetStatusFlags.STATUS_FLAG_IN_ALARM)
                    //            status_text = "Hiba";
                    //        else if ((status & BacnetStatusFlags.STATUS_FLAG_OUT_OF_SERVICE) == BacnetStatusFlags.STATUS_FLAG_OUT_OF_SERVICE)
                    //            status_text = "Használaton kívül";
                    //        else if ((status & BacnetStatusFlags.STATUS_FLAG_OVERRIDDEN) == BacnetStatusFlags.STATUS_FLAG_OVERRIDDEN)
                    //            status_text = "Felülbírálva";
                    //        else
                    //            status_text = "Valami más történt:";
                    //    }
                    //    if (status_text != "")
                    //    {
                    //        Value = status_text;

                    //        Program.events.Add(timestamp + ": " + Event + " " + initiatingDeviceIdentifier + " " + monitoredObjectIdentifier + " " + Value);
                    //        logToListbox(timestamp + ": " + Event + " " + initiatingDeviceIdentifier + " " + monitoredObjectIdentifier + " " + Value);
                    //        //WriteFile("SavedList_Event.txt", timestamp + " " + Event + " " + initiatingDeviceIdentifier.ToString() + " " + monitoredObjectIdentifier.ToString() + " " + Value, 10, 10);
                    //    }
                    //    break;

                    default:
                        rawData = "";
                        break;
                }
            }
            if (need_confirm)
            {
                sender.SimpleAckResponse(adr, BacnetConfirmedServices.SERVICE_CONFIRMED_COV_NOTIFICATION, invoke_id);
            }
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
            }

            return "";
        }

        public static string convertToIP(BacnetAddress adr)
        {
            var address = adr.adr;
            Array.Resize(ref address, 4);

            return new System.Net.IPAddress(address).ToString();
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

        #endregion

    }

    //class asd : BaCSharpObject
    //{

    //}
}
