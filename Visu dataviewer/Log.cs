using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Timers;

namespace Visu_dataviewer
{
    public static class Log
    {
        public static bool inProgress;
        static List<string> logMessages;

        static Log()
        {
            logMessages = new List<string>();
            Timer logWriterTimer = new Timer();
            logWriterTimer.Elapsed += new ElapsedEventHandler(writeLogFile);
            logWriterTimer.Interval = 500;
            logWriterTimer.Enabled = true;
        }

        public static void Append(string message)
        {
            inProgress = true;
            logMessages.Add(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff") + ": " + message);
        }

        public static void writeLogFile(object source, ElapsedEventArgs e)
        {
            var path = Path.Combine(@"c:\Visu\Logs\", global.Now + ".txt");
            var len = logMessages.Count;
            
            if (len > 0)
            {
                var listToWrite = logMessages.GetRange(0, len);
                File.AppendAllLines(path, listToWrite);
                logMessages.RemoveRange(0, len);
                inProgress = false;
            }
        }
    }
}
