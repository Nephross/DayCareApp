using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayCareApp.Entities.Placeholder
{
   public class DBinitializer
    {
        public SqlConnection Connection = null;

        private static void OpenSqlConnection(SqlConnection connection)
        {


            connection.Open();
            Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
            Console.WriteLine("State: {0}", connection.State);


            // Connection.Close();

        }

        static private string GetConnectionString()
        {
            // To avoid storing the Connection string in your code, 
            // you can retrieve it from a configuration file, using the 
            // System.Configuration.ConfigurationSettings.AppSettings property 
            return @"Server=TEIS-PC\SQLDB2016;Database=DAYCAREAPPDB;Trusted_Connection=Yes;"
                + "Integrated Security=SSPI;";
        }

        public DBinitializer()
        {
            Connection = new SqlConnection(GetConnectionString());
            OpenSqlConnection(Connection);
            Console.Write("SUCCES!");
        }

        public void DbCommand(string commandString)
        {
            SqlCommand myCommand = new SqlCommand(commandString, Connection);

            myCommand.ExecuteNonQuery();
            Console.WriteLine("INSERTED INTO DB");
        }

        public void DbReadOrderData()
        {

            string queryString = "SELECT  Name, Country FROM dbo.Child;";
            SqlCommand myCommand = new SqlCommand(queryString, Connection);
            //Connection.Open();

            SqlDataReader reader = myCommand.ExecuteReader();

            Console.WriteLine("Reading from db");

            // Call Read before accessing data.
            while (reader.Read())
            {
                ReadSingleRow((IDataRecord)reader);
            }

            // Call Close when done reading.
            reader.Close();
        }


        private static void ReadSingleRow(IDataRecord record)
        {
            Console.WriteLine(String.Format("{0}, {1}", record[0], record[1]));
        }

    }
}
