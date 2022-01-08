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
        public static List<string> Error = new List<string>();
        public static Dictionary<string, BacnetObjectTypes> BacnetObject;
        public static BacnetClient BacnetClient;

        public static bool StartActivity(string localEndpoint, uint writePriority)
        {
            try
            {
                var endpoint = new BacnetIpUdpProtocolTransport(0xBAC0, false, false, 1472, localEndpoint);
                BacnetClient = new BacnetClient(endpoint) {WritePriority = writePriority};
                BacnetClient.OnCOVNotification += handler_OnCOVNotification;
                BacnetClient.Start();
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

        public static BacnetObjectId GetBacnetObject(string objectType, uint instance)
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

        public static BacnetAddress GetBacnetDevice(string ipAddress, ushort networkNumber)
        {
            return new BacnetAddress(BacnetAddressTypes.IP, ipAddress, networkNumber);
        }
        
         public static BacnetValue[] GetBacnetValue(BacnetObjectId bacnetObject, string value, string format, bool reset)
        {
            var bacnetValueArray = new BacnetValue[] { new BacnetValue() };
            uint? uintNullvalue = null;
            float? singleNullvalue = null;
            switch (format)
            {
                case "binary": bacnetValueArray[0].Value = reset ? uintNullvalue : Convert.ToUInt32(value); bacnetValueArray[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_ENUMERATED; break;
                case "int": bacnetValueArray[0].Value = reset ? uintNullvalue : Convert.ToUInt32(value); bacnetValueArray[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_UNSIGNED_INT; break;
                case "float": bacnetValueArray[0].Value = reset ? singleNullvalue : Convert.ToSingle(value); bacnetValueArray[0].Tag = BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL; break;
            }
            return bacnetValueArray;
        }

        public static void handler_OnCOVNotification(BacnetClient sender, BacnetAddress bacnetDevice, byte invoke_id, uint subscriberProcessIdentifier, BacnetObjectId initiatingDeviceIdentifier, BacnetObjectId bacnetObject, uint timeRemaining, bool need_confirm, ICollection<BacnetPropertyValue> values, BacnetMaxSegments max_segments)
        {
            if ((BacnetPropertyIds)values.ToList()[0].property.propertyIdentifier == BacnetPropertyIds.PROP_PRESENT_VALUE)
                Datapoints.Record(bacnetDevice, bacnetObject, values.ToList()[0].value[0].ToString());

            if (need_confirm)
                sender.SimpleAckResponse(bacnetDevice, BacnetConfirmedServices.SERVICE_CONFIRMED_COV_NOTIFICATION, invoke_id);
        }
    }
}
