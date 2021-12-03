using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Threading;


namespace Visu_dataviewer
{
    public static class Log
    {
        public static void append(string message)
        {

            //var actualLog = @"c:\Visu\Logs\" + _global.now + ".txt";
            var actualLog = Path.Combine(@"c:\Visu\Logs\", _global.now + ".txt");
            File.AppendAllText(actualLog, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff") + ": " + message + Environment.NewLine);

        }
    }
}
