using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.BACnet;
namespace Visu_dataviewer
{
    public class Device
    {
        public ushort Network { get; set; }
        public string IP { get; set; }
        public uint Instance { get; set; }

        public Device(ushort network, string ip, uint instance)
        {
            this.Network = network;
            this.IP = ip;
            this.Instance = instance;

        }

        public BacnetAddress GetDevice()
        {
            return new BacnetAddress(BacnetAddressTypes.IP, IP, Network);
        }
    }
}
