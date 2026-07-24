using System;
using System.Collections.Generic;
using System.Text;
using VehiculosExpress.Models;
using VehiculosExpress.Services;
using VehiculosExpress.Repository;
using Spectre.Console;

namespace VehiculosExpress.Screens
{
    internal class MenuPrincipal
    {
        private readonly VehiculoService _vehiculoService;

        public MenuPrincipal(VehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        public void MostrarMenu()
        {

            AnsiConsole.Write(new FigletText("Carlos").Centered().Color(Color.White));
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new FigletText("Renta Car").Centered().Color(Color.Blue));

            bool salir = false;
            while (!salir)

            {
                
                var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orange1]Seleccione una opción:[/]")
                    .AddChoices(
                    "Agregar Vehículo",
                    "Ver Vehículos",
                    "Actualizar Vehículo",
                    "Eliminar Vehículo",
                    "Salir"));

              
                switch (opcion)
                {
                    case "Agregar Vehículo":
                        RegistrarVehiculoScreen();
                        break;

                    case "Ver Vehículos":
                        
                        VerVehiculosScreen();
                        break;

                    case "Actualizar Vehículo":
                        ActualizarVehiculoScreen();
                        break;

                    case "Eliminar Vehículo":
                        EliminarVehiculoScreen(); 
                        break;
                    case "Salir":
                        salir = true;
                        break;
                }
                
                
                Console.ReadKey();
            }

        }
        private void RegistrarVehiculoScreen()
        {
            AnsiConsole.MarkupLine("[bold yellow]Registrar Vehículo[/]");
            AnsiConsole.MarkupLine("[green]Ingrese los datos del vehículo:[/]\n");
            var marca = AnsiConsole.Ask<string>("[green]Marca:[/]");
            var modelo = AnsiConsole.Ask<string>("[green]Modelo:[/]");
            var color = AnsiConsole.Ask<string>("[green]Color:[/]");
            var anio = AnsiConsole.Ask<int>("[green]Año:[/]");

            AnsiConsole.MarkupLine("[yellow]Agregando vehículo...[/]");

            AnsiConsole.Status()
               .Start("...", ctx =>
               {
                   Thread.Sleep(3000);
               });

            var vehiculo = new Vehiculos
            {
                Marca = marca,
                Modelo = modelo,
                Color = color,
                Anio = anio
            };
            _vehiculoService.AgregarVehiculo(vehiculo);
            AnsiConsole.MarkupLine("[green]Vehículo agregado exitosamente.[/]");
            
        }

        public void VerVehiculosScreen()
        {
            AnsiConsole.Clear(); 

            var tabla = new Table()
                       .BorderStyle(new Style(Color.Orange1))
                       .Border(TableBorder.Rounded)
                       .AddColumn("[yellow]ID[/]")
                       .AddColumn("[yellow]Marca[/]")
                       .AddColumn("[yellow]Modelo[/]")
                       .AddColumn("[yellow]Color[/]")
                       .AddColumn("[yellow]Año[/]");

            foreach (var v in _vehiculoService.ObtenerVehiculos())
            {
                tabla.AddRow(
                    v.Id.ToString(),
                    v.Marca,
                    v.Modelo,
                    v.Color,
                    v.Anio.ToString()
                );
            }
            AnsiConsole.Write(tabla);

        }
        public void ActualizarVehiculoScreen()
        {
        int id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID del vehículo a actualizar:[/]");
            var vehiculo = new Vehiculos
            {
                Id = id,
                Marca = AnsiConsole.Ask<string>("[green]Nueva marca:[/]"),
                Modelo = AnsiConsole.Ask<string>("[green]Nuevo modelo:[/]"),
                Color = AnsiConsole.Ask<string>("[green]Nuevo color:[/]"),
                Anio = AnsiConsole.Ask<int>("[green]Nuevo año:[/]")
            };
            AnsiConsole.MarkupLine("[green]Actualizando vehiculo...[/]");

            AnsiConsole.Status()
               .Start("...", ctx =>
               {
                   Thread.Sleep(3000);
               });

            _vehiculoService.ActualizarVehiculo(vehiculo);
            AnsiConsole.MarkupLine("[green]Vehiculo actualizado exitosamente.[/]");
        }


        public void EliminarVehiculoScreen()
        {
            int id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID del vehículo a eliminar:[/]");
            if (AnsiConsole.Confirm($"[yellow]¿Está seguro de que desea eliminar el vehículo con ID [/][red]{id}?[/]\n"))
            {
                AnsiConsole.MarkupLine("[red]Eliminando vehiculo...[/]");
                AnsiConsole.Status()
               .Start("...", ctx =>
               {
                   Thread.Sleep(3000);
               });

                _vehiculoService.EliminarVehiculo(id);
                AnsiConsole.MarkupLine("[red]Vehiculo eliminado exitosamente.[/]");
            }else{
                AnsiConsole.MarkupLine("[yellow]Operación cancelada.[/]");
                return;
            }
            

            
        }



    }
          

}
