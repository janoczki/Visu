using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Visu_dataviewer
{
    public static class _global
    {
        public static string path;
        public static uint covLifetime;
        public static List<List<string>> bigDatapointTable;
        public static string config;
        static _global()
        {
            bigDatapointTable = new List<List<string>>();
        }

        public static void ini()
        {
            string[] file = File.ReadAllLines(@"C:\Visu\config.cfg", Encoding.Default);
            path = file[0];
            covLifetime = uint.Parse(file[1]);
        }
    }
}
