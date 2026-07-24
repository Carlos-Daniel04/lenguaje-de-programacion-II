using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;


namespace VehiculosExpress.Database
{
    internal class DatabaseInicializar
    {
        private readonly string _connection;

        public DatabaseInicializar(string connection)
        {
            _connection = connection;
        }

        public void Inicializar()
        {
            using var inicio = new SQLiteConnection(_connection);
            
                inicio.Open();

                string sql = @"
                    CREATE TABLE IF NOT EXISTS Vehiculos (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Marca TEXT NOT NULL,
                        Modelo TEXT NOT NULL,
                        Color TEXT NOT NULL,
                        Anio INTEGER NOT NULL
                    );";
                 using var command = new SQLiteCommand(sql, inicio);
                
                    command.ExecuteNonQuery();
                
            


    
        }

    }

}

