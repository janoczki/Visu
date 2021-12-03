using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
namespace Visu_dataviewer
{
    public static class _global
    {
        public static string now;
        public static string path;
        public static uint covLifetime;
        public static List<List<string>> bigDatapointTable;
        
        public static string config;
        public static List<List<string>> dataTransfer;
        public static int cycleCounter = 1;
        static _global()
        {
            bigDatapointTable = new List<List<string>>();
            now = DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss");
            dataTransfer = new List<List<string>>();

            var dataTransferTimer = new Timer();
            dataTransferTimer.Interval = 2000;
            dataTransferTimer.Tick += new EventHandler(dataTransferTimer_Tick);
            dataTransferTimer.Enabled = true;
        }

        private static void dataTransferTimer_Tick(Object myObject, EventArgs myEventArgs)
        {
            var count = dataTransfer.Count;
            if (count > 0)
            {
                Sql.writeList(dataTransfer, count);
                dataTransfer.RemoveRange(0, count);
            }
        }

        public static void ini()
        {
            string[] file = File.ReadAllLines(@"C:\Visu\config.cfg", Encoding.Default);
            path = file[0];
            covLifetime = uint.Parse(file[1]);
        }

        public enum prop
        {
            datapointName,
            datapointDescription,
            datapointDatatype,
            datapointSave,
            datapointCOV,
            deviceIP,
            deviceInstance,
            objectType,
            objectInstance,
            value,
            activeText,
            inactiveText,
            stateText,
            available,
            dayMo,
            dayTu,
            dayWe,
            dayTh,
            dayFr,
            daySa,
            daySu
        }

        public static string normalizeNumber(string number, int len)
        {
            var ret = new string('0', len - number.Length) + number;
            return ret;
        }
    }
}
