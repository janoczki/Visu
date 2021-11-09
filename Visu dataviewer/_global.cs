using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace Visu_dataviewer
{
    public static class _global
    {
        public static string now;
        public static string path;
        public static uint covLifetime;
        public static List<List<string>> bigDatapointTable;
        public static string config;
        public static BackgroundWorker asd;
        public static BackgroundWorker fgh;


        static _global()
        {
            bigDatapointTable = new List<List<string>>();
            now = DateTime.Now.ToString("yyyy.MM.dd hh.mm.ss");
        }

        public static void ini()
        {
            string[] file = File.ReadAllLines(@"C:\Visu\config.cfg", Encoding.Default);
            path = file[0];
            covLifetime = uint.Parse(file[1]);
        }
    }
}
