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
        public BacnetAddress DeviceId { get; set; }
        public BacnetObjectId ObjectId { get; set; }
        private IList<BacnetValue> _response;

        public NormalObject(BacnetAddress deviceId, BacnetObjectId objectId)
        {
            this.ObjectId = objectId;
            this.DeviceId = deviceId;
        }

        public string Read()
        {
            try
            {
                Bac.BacnetClient.ReadPropertyRequest(
                    DeviceId, ObjectId, BacnetPropertyIds.PROP_PRESENT_VALUE, 
                    out _response);
            }
            catch (Exception ex)
            {
                Log.Append(
                    "Read failed " + DeviceId.adr + " " + 
                    ObjectId.type + " " + ObjectId.instance + 
                    " Reason: " + ex.Message);
            }
            return _response[0].Value != null ? _response[0].Value.ToString() : "null";
        }

        public void Write(dynamic value, string format, bool reset)
        {
            try
            {
                Bac.BacnetClient.WritePropertyRequest(
                    DeviceId, ObjectId, BacnetPropertyIds.PROP_PRESENT_VALUE, 
                    Bac.GetBacnetValue(ObjectId, value, format, reset));
            }
            catch (Exception ex)
            {
                Log.Append(
                    "Write failed " + DeviceId.adr + " " + 
                    ObjectId.type + " " + ObjectId.instance + 
                    " Reason: " + ex.Message);
            }
        }

        public void Subscribe()
        {
            try
            {
                Bac.BacnetClient.SubscribeCOVRequest(
                    DeviceId, ObjectId, 0, false, 
                    false, global.CovLifetime);
            }
            catch (Exception ex)
            {
                var failureReason = ex.ToString();
                Log.Append(
                    "Subscription failed " + DeviceId.adr + " " + 
                    ObjectId.type + " " + ObjectId.instance + 
                    " Reason: " + failureReason);
            }
        }
    }
}
