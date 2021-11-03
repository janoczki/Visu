using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visu_dataviewer
{
    public static class _global
    {
        public static string path;
        public static List<List<string>> bigDatapointTable;

        static _global()
        {
            bigDatapointTable = new List<List<string>>();
        }
    }
}
