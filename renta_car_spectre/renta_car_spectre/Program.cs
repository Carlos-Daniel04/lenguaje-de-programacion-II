using System;
using Spectre.Console;
using renta_car_spectre.Services;
using renta_car_spectre.models;

class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.Write(new FigletText("Carlos").Centered().Color(Color.White));
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new FigletText("Renta Car").Centered().Color(Color.Blue));
        var servicio = new VehiculoService();
        bool salir = false;

        while (!salir)
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orange1]Seleccione una opción:[/]")
                    .AddChoices(
                        "Ver vehiculos",
                        "Registrar vehiculo",
                        "Eliminar vehiculo",
                        "Rentar vehiculo",
                        "Devolver vehiculo",
                        "Salir"
                    ));

            switch (opcion)
            {
                case "Ver vehiculos":
                    AnsiConsole.Clear();

                    var table = new Table()
                    .BorderStyle(new Style(Color.Orange1))
                    .Border(TableBorder.Rounded)
                    .AddColumn("[yellow]ID[/]")
                    .AddColumn("[yellow]Marca[/]")
                    .AddColumn("[yellow]Modelo[/]")
                    .AddColumn("[yellow]Color[/]")
                    .AddColumn("[yellow]Año[/]")
                    .AddColumn("[yellow]Precio por día[/]")
                    .AddColumn("[yellow]Disponible[/]");

                    foreach (var v in servicio.GetVehiculos())
                    {
                        table.AddRow(
                            v.Id.ToString(),
                            v.Marca,
                            v.Modelo,
                            v.Color,
                            v.Ano.ToString(),
                            v.PrecioPorDia.ToString("C"),
                            v.Disponible ? "[green]Sí[/]" : "[red]No[/]"
                        );
                    }

                    AnsiConsole.Write(table);
                    break;

                case "Registrar vehiculo":

                    servicio.RegistrarVehiculo();
                    break;

                case "Eliminar vehiculo":

                    servicio.EliminarVehiculo();
                    break;

                case "Rentar vehiculo":

                    int idRenta = AnsiConsole.Ask<int>("[blue]Ingrese el ID del vehículo a rentar:[/]");
                    servicio.RentarVehiculo(idRenta);
                    break;

                case "Devolver vehiculo":

                    int idDevolver = AnsiConsole.Ask<int>("[blue]Ingrese el ID del vehículo a devolver:[/]");
                    servicio.DevolverVehiculo(idDevolver);
                    break;

                case "Salir":
                    salir = true;
                    break;
            }
        }

        Console.ReadKey();
    }
    
}