using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.BACnet;
using System.Threading;
using System.Windows.Forms;
namespace Visu_dataviewer
{
    public class Bac
    {
        public static BacnetClient bacnet_client;

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
            }

            bacnetobj.Instance = instance;

            return bacnetobj;
        }

        public static BacnetAddress bacnetDevice(string ipAddress, ushort networkNumber)
        {
            BacnetAddress bacnetDev = new BacnetAddress(BacnetAddressTypes.IP, ipAddress, networkNumber);
            return bacnetDev;
        }

        public static string readValue(ushort networkNumber, string deviceIP, uint deviceInstance, string objectType, uint objectInstance, string property)
        {
            BacnetValue Value;
            IList<BacnetValue> NoScalarValue;
            try
            {
                bacnet_client.ReadPropertyRequest(
                    bacnetDevice(deviceIP, networkNumber),
                    bacnetNode(objectType, objectInstance),
                    getPropertyId(property),
                    out NoScalarValue);
                Value = NoScalarValue[0];
                return Value.Value.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static bool SubscribeToCoV(ushort networkNumber, string deviceIP, uint deviceInstance, string objectType, uint objectInstance, uint duration)
        {
            bool cancel = false;
            if (duration == 0) cancel = true;

            bacnet_client.SubscribeCOVRequest(
                bacnetDevice(deviceIP, networkNumber),
                bacnetNode(objectType, objectInstance), 0, cancel, false, duration);

            return true;
        }

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

        public static void handler_OnCOVNotification(BacnetClient sender, BacnetAddress adr, byte invoke_id, uint subscriberProcessIdentifier, BacnetObjectId initiatingDeviceIdentifier, BacnetObjectId monitoredObjectIdentifier, uint timeRemaining, bool need_confirm, ICollection<BacnetPropertyValue> values, BacnetMaxSegments max_segments)
        {
            //BacnetAddress asd = new BacnetAddress(BacnetAddressTypes.IP,)
            
            //var asdasd = adr
            var objInstance = monitoredObjectIdentifier.Instance;
            var objType = customTypeFromBacnetObjectType(monitoredObjectIdentifier.Type);
            var remainingTime = timeRemaining;


            foreach (BacnetPropertyValue value in values)
            {
                switch ((BacnetPropertyIds)value.property.propertyIdentifier)
                {
                    case BacnetPropertyIds.PROP_PRESENT_VALUE:
                        //Event = "Értékváltozás";
                        var Value = value.value[0].ToString();
                        
                        foreach(var item in _global.bigDatapointTable)
                        {
                            if ((item[7] == objType) & 
                                (item[8] == objInstance.ToString()))
                            {
                                item[9] = Value;
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
                    //        // TODO: egyszerre kapom meg a statusflageket, így egyszerre kell visszaadnom az állapotokat
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
                        Value = "";
                        break;
                }
            }
            if (need_confirm)
            {
                sender.SimpleAckResponse(adr, BacnetConfirmedServices.SERVICE_CONFIRMED_COV_NOTIFICATION, invoke_id);
            }
        }
    }
}
