using VehiculosExpress.Screens;
using VehiculosExpress.Services;
using VehiculosExpress.Repository;
using VehiculosExpress.Database;

class Program
{
    static void Main(string[] args)
    {
        string connection = "Data Source=vehiculos.db;Version=3;";

        var inicializador = new DatabaseInicializar(connection);
        inicializador.Inicializar();
        var conexion = new DatabaseConexion(connection);
        var vehiculosRepository = new VehiculosRepository(conexion);
        var servicios = new VehiculoService(vehiculosRepository);
        var menu = new MenuPrincipal(servicios);

        menu.MostrarMenu();

    }
}