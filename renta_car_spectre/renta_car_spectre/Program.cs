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
        var servicioCliente = new ClienteSevice();
        bool salir = false;

        while (!salir)
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[orange1]Seleccione una opción:[/]")
                    .AddChoices(
                        "Ver clientes",
                        "Registrar cliente",
                        "Eliminar cliente",
                        "Ver vehiculos",
                        "Registrar vehiculo",
                        "Eliminar vehiculo",
                        "Rentar vehiculo",
                        "Devolver vehiculo",
                        "Salir"
                    ));

            switch (opcion)
            {
                case "Ver clientes":
                    AnsiConsole.Clear();

                    var tableClientes = new Table()
                    .BorderStyle(new Style(Color.Orange1))
                    .Border(TableBorder.Rounded)
                    .AddColumn("[yellow]ID[/]")
                    .AddColumn("[yellow]Nombre[/]")
                    .AddColumn("[yellow]Apellido[/]")
                    .AddColumn("[yellow]Cédula[/]")
                    .AddColumn("[yellow]Teléfono[/]");

                    foreach (var c in servicioCliente.GetClientes())
                    {
                        tableClientes.AddRow(
                            c.Id.ToString(),
                            c.Nombre,
                            c.Apellido,
                            c.Cedula.ToString(),
                            c.Telefono.ToString()
                        );
                    }
                    AnsiConsole.Write(tableClientes);
                    break;  

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
                            v.Disponible ? "[green]Sí[/]"  : "[red]No[/]"

                        );
                    }

                    AnsiConsole.Write(table);
                    break;

                case "Registrar cliente":

                    servicioCliente.RegistrarCliente();
                    break;


                case "Eliminar cliente":

                    servicioCliente.EliminarCliente();
                    break;


                case "Registrar vehiculo":

                    servicio.RegistrarVehiculo();
                    break;


                case "Eliminar vehiculo":

                    servicio.EliminarVehiculo();
                    break;


                case "Rentar vehiculo":


                    int idRenta = AnsiConsole.Ask<int>("[blue]Ingrese el ID del vehículo a rentar:[/]");
                    int idCliente = AnsiConsole.Ask<int>("[blue]Ingrese el ID del cliente:[/]");

                    var clienteEncontrado = servicioCliente.BuscarClientePorId(idCliente);

                    if (clienteEncontrado == null)
                    {
                        AnsiConsole.MarkupLine("[yellow]No se encontró un cliente con ese ID.[/]\n");

                        if (AnsiConsole.Confirm("¿Desea registrar un nuevo cliente ahora?"))
                        {
                            clienteEncontrado = servicioCliente.RegistrarCliente();
                            servicio.RentarVehiculo(idRenta, clienteEncontrado.Id);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]No se pudo completar la renta sin un cliente registrado.[/]\n");
                        }
                    }
                    else
                    {
                        servicio.RentarVehiculo(idRenta, clienteEncontrado.Id);
                    }

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