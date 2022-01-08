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
    public static class global
    {
        public static string Now;
        public static string Path;
        public static uint CovLifetime;
        public static int PollInterval;
        public static string LocalEndPoint;
        public static uint WritePriority;
        public static void Ini()
        {
            Now = DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss");
            string[] file = File.ReadAllLines(@"C:\Visu\config.cfg", Encoding.Default);
            Path = file[0];
            CovLifetime = uint.Parse(file[1]);
            PollInterval = int.Parse(file[2]);
            LocalEndPoint = file[3];
            WritePriority = uint.Parse(file[4]);
        }
    }
}
