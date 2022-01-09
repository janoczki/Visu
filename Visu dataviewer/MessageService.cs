using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Visu_dataviewer
{
    class MessageService
    {
        public static void SendWarninglMessage(string message, string title)
        {
            SendMessage(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }

        public static void SendErrolMessage(string message, string title)
        {
            SendMessage(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void SendInfoMessage(string message, string title)
        {
            SendMessage(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void SendMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, buttons, icon);
        }
    }
}
