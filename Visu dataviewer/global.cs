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
        public static string now;
        public static string path;
        public static uint covLifetime;
        public static int pollInterval;

        public static void ini()
        {
            now = DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss");
            string[] file = File.ReadAllLines(@"C:\Visu\config.cfg", Encoding.Default);
            path = file[0];
            covLifetime = uint.Parse(file[1]);
            pollInterval = int.Parse(file[2]);
        }
    }
}
