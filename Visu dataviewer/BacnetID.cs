using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.BACnet;
namespace Visu_dataviewer
{
    public class BacnetID
    {
        public BacnetAddress Device { get; set; }
        public BacnetObjectId Object { get; set; }
        public string ObjectType { get; set; }
        public uint ObjectInstance { get; set; }

        public BacnetID(BacnetAddress device, string objectType, uint objectInstance)
        {
            this.ObjectType = objectType;
            this.ObjectInstance = objectInstance;
        }

        public BacnetObjectId GetBacnetObject()
        {
            BacnetObjectId bacnetobj = new BacnetObjectId();
            var type = new Dictionary<string, BacnetObjectTypes>()
            {
                { "AI", BacnetObjectTypes.OBJECT_ANALOG_INPUT },            { "AO", BacnetObjectTypes.OBJECT_ANALOG_OUTPUT },       { "AV", BacnetObjectTypes.OBJECT_ANALOG_VALUE },
                { "BI", BacnetObjectTypes.OBJECT_BINARY_INPUT },            { "BO", BacnetObjectTypes.OBJECT_BINARY_OUTPUT },       { "BV", BacnetObjectTypes.OBJECT_BINARY_VALUE },
                { "MI", BacnetObjectTypes.OBJECT_MULTI_STATE_INPUT },       { "MO", BacnetObjectTypes.OBJECT_MULTI_STATE_OUTPUT },  { "MV", BacnetObjectTypes.OBJECT_MULTI_STATE_VALUE },
                { "PIV", BacnetObjectTypes.OBJECT_POSITIVE_INTEGER_VALUE }, { "SC", BacnetObjectTypes.OBJECT_SCHEDULE },            { "DEV", BacnetObjectTypes.OBJECT_DEVICE }
            };
            type.TryGetValue(ObjectType, out bacnetobj.type);
            bacnetobj.Instance = ObjectInstance;
            return bacnetobj;
        }
    }
}
