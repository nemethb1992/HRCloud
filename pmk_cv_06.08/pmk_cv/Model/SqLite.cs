using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Model
{
    public class SqLite
    {
        private const string CONNECTION_URL = "Data Source = innerDatabase.db";
        protected SQLiteConnection conn;
        public SqLite()
        {
            conn = new SQLiteConnection(CONNECTION_URL);
        }
        protected void connectionOpen()
        {
            conn.Open();
        }
        protected void connectionClose()
        {
            conn.Close();
        }
        public void update(string query)
        {
            var command = conn.CreateCommand();
            connectionOpen();
            command.CommandText = query;
            command.ExecuteNonQuery();
            connectionClose();
        }
        public string query(string query)
        {
            connectionOpen();
            var command = conn.CreateCommand();
            command.CommandText = query;
            SQLiteDataReader sdr = command.ExecuteReader();
            string data = "";
            while (sdr.Read())
            {
                data = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            connectionClose();
            return data;
        }
    }
}
