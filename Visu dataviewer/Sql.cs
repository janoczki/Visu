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



        public static string lofasz()
        {
            return builder.ConnectionString;
        }

        public static void connect()
        {
            connection.ConnectionString = builder.ConnectionString;
            connection.Open();
            //read();
        }

        public static void write(string name, string value, string timestamp)
        {
            timestamp = timestamp.Replace('.', '-');
            timestamp = timestamp.Replace("- "," ");
            string queryString =
                "INSERT INTO [database].[dbo].[datatable] ([Datapoint name], [Datapoint value], Timestamp) VALUES('" + name + "', '" + value + "', '" + timestamp + "'); ";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.ExecuteNonQuery();

        }
        public static string read()
        {
            string queryString = "SELECT * FROM [database].[dbo].[datatable]";

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
