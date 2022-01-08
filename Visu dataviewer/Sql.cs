using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Visu_dataviewer
{
    public static class Sql
    {

        public static SqlConnectionStringBuilder Builder = new SqlConnectionStringBuilder();
        public static SqlConnection Connection = new SqlConnection();
        public static List<List<string>> DataTransfer = new List<List<string>>();
        static Sql()
        {
            Builder.InitialCatalog = "database";
            Builder.DataSource = "ANDRASGEP\\SQLEXPRESS";
            Builder.UserID = "sa";
            Builder.Password = "Sauter12345";
            var dataTransferTimer = new Timer {Interval = 2000, Enabled = true};
            dataTransferTimer.Tick += dataTransferTimer_Tick;
        }

        public static void Connect()
        {
            Connection.ConnectionString = Builder.ConnectionString;
            Connection.Open();
        }

        public static void AddToDataTransferList(List<string> data)
        {
            DataTransfer.Add(data);
        }

        public static void WriteList(List<List<string>> list,int quantity)
        {
            var allDataRow = "";
            for (int i = 0; i < quantity; i++)
            {
                var dataRow = String.Join("','", list[i]);
                var separator = (i == quantity - 1) ? "" : ",";

                dataRow = "('" + dataRow + "')" + separator; 
                allDataRow += dataRow;
            }
            allDataRow += ";";

            var queryString = "INSERT INTO [database].[dbo].[table] ([Datapoint name], [Datapoint value], Timestamp) VALUES " + allDataRow;
            var command = new SqlCommand(queryString, Connection);
            command.ExecuteNonQuery();
        }

        private static void dataTransferTimer_Tick(Object myObject, EventArgs myEventArgs)
        {
            var count = DataTransfer.Count;
            if (count <= 0) return;
            WriteList(DataTransfer, count);
            DataTransfer.RemoveRange(0, count);
        }
    }
}
