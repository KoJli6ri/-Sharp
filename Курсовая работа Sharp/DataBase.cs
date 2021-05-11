using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Курсовая_работа_Sharp
{
    class DataBase
    {
        MySqlConnection connection = new MySqlConnection("server = remotemysql.com; port = 3306; Username = nMgb5JJ49J; Password = 29T494V9pn; database = nMgb5JJ49J; charset = utf8");

        public void openConn()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConn()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public MySqlConnection getConn()
        {
            return connection;
        }
    }
}
