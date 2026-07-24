using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace VehiculosExpress.Database
{
    internal class DatabaseConexion
    {
        private readonly string _connection;

        public DatabaseConexion(string connection)
        {
            _connection = connection;
        }

        public SQLiteConnection CrearConexion()
        {
            var connection = new SQLiteConnection(_connection);
            connection.Open();
            return connection;
        }
    }
}
