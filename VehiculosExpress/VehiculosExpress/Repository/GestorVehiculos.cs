using System;
using System.Collections.Generic;
using System.Text;
using VehiculosExpress.Models;
using System.Data.SQLite;
using VehiculosExpress.Database;

namespace VehiculosExpress.Repository
{
    internal class VehiculosRepository
    {

        private readonly DatabaseConexion _databaseConexion;
        public VehiculosRepository(DatabaseConexion databaseConexion)
        {
            _databaseConexion = databaseConexion;
        }

        public void AgregarVehiculo(Vehiculos vehiculo)
        {
            using (var connection = new SQLiteConnection(_databaseConexion.CrearConexion()))
            {
                

                string insertQuery = "INSERT INTO Vehiculos (Marca, Modelo, Color, Anio) VALUES (@Marca, @Modelo, @Color, @Anio)";
                using var command = new SQLiteCommand(insertQuery, connection);
                {
                    command.Parameters.AddWithValue("@Marca", vehiculo.Marca);
                    command.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                    command.Parameters.AddWithValue("@Color", vehiculo.Color);
                    command.Parameters.AddWithValue("@Anio", vehiculo.Anio.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Vehiculos> ObtenerVehiculos()
        {
            var ListaVehiculos = new List<Vehiculos>();

            using var conexion = _databaseConexion.CrearConexion();
            

                string comandos = "SELECT * FROM Vehiculos";
                using var command = new SQLiteCommand(comandos, conexion);

                using var reader = command.ExecuteReader();
                    
                        while (reader.Read())
                        {
                            var vehiculo = new Vehiculos
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Marca = reader["Marca"].ToString(),
                                Modelo = reader["Modelo"].ToString(),
                                Color = reader["Color"].ToString(),
                                Anio = Convert.ToInt32(reader["Anio"])
                            };
                            ListaVehiculos.Add(vehiculo);
                        }
                    
                
            

            return ListaVehiculos;
        }

        public void ActualizarVehiculo(Vehiculos vehiculo)
        {
            using (var conexion =_databaseConexion.CrearConexion())
            {
                

                string updateQuery = "UPDATE Vehiculos SET Marca = @Marca, Modelo = @Modelo, Color = @Color, Anio = @Anio WHERE Id = @Id";
                using (var command = new SQLiteCommand(updateQuery, conexion))
                {
                    command.Parameters.AddWithValue("@Marca", vehiculo.Marca);
                    command.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                    command.Parameters.AddWithValue("@Color", vehiculo.Color);
                    command.Parameters.AddWithValue("@Anio", vehiculo.Anio.ToString());
                    command.Parameters.AddWithValue("@Id", vehiculo.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarVehiculo(int id)
        {
            using (var conexion = _databaseConexion.CrearConexion())
            {
                //conexion.Open();

                string deleteQuery = "DELETE FROM Vehiculos WHERE Id = @Id";

                using (var command = new SQLiteCommand(deleteQuery, conexion))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
