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

        public static SqlConnectionStringBuilder builder;
        public static SqlConnection connection;
        static Sql()
        {
            builder = new SqlConnectionStringBuilder();
            connection = new SqlConnection();
            //builder.ConnectionString = "server=(local);user id=ab;" +
            //"password= a!Pass113;initial catalog=AdventureWorks";
            builder.InitialCatalog = "database";
            builder.DataSource = "ANDRASGEP\\SQLEXPRESS";
            builder.UserID = "sa";
            builder.Password = "Sauter12345";
        }

        public static void connect()
        {
            connection.ConnectionString = builder.ConnectionString;
            connection.Open();
        }

        public static void writeList(List<List<string>> list,int quantity)
        {
            
            var allDataRow = "";
            for (int i = 0; i < quantity; i++)
            {
                //var dataRow = "";
                var dataRow = String.Join("','", list[i]);
                //dataRow = "VALUES(" + dataRow + ")";
                if (i != quantity - 1)
                {
                    dataRow = "('" + dataRow + "')," + Environment.NewLine;
                }
                else
                {
                    dataRow = "('" + dataRow + "')" + Environment.NewLine;
                }
                allDataRow = allDataRow + dataRow;
            }

            allDataRow = allDataRow + ";";
            
            


            string queryString =
                "INSERT INTO [database].[dbo].[table] ([Datapoint name], [Datapoint value], Timestamp) VALUES " + allDataRow;

            SqlCommand command = new SqlCommand(queryString, connection);
            command.ExecuteNonQuery();
        }

        public static void write(string name, string value, string timestamp)
        {
            timestamp = timestamp.Replace('.', '-');
            timestamp = timestamp.Replace("- "," ");
            string queryString =
                "INSERT INTO [database].[dbo].[table] ([Datapoint name], [Datapoint value], Timestamp) VALUES('" + name + "', '" + value + "', '" + timestamp + "'); ";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.ExecuteNonQuery();
        }

        public static string read()
        {
            string queryString = "SELECT * FROM [database].[dbo].[table]";
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    MessageBox.Show(reader[0].ToString());
                }
            }
            finally
            {
                reader.Close();
            }
            return "";
        }


    }
}
