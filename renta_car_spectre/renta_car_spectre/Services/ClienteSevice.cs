using renta_car_spectre.models;
using renta_car_spectre.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace renta_car_spectre.Services
{
    internal class ClienteSevice
    {
        private int ultimoId = 1;
        private List<Cliente> clientes = new List<Cliente>();

        public Cliente RegistrarCliente()
        {
            string nombre = AnsiConsole.Ask<string>("[green]Ingrese el nombre del cliente:[/]");
            string apellido = AnsiConsole.Ask<string>("[green]Ingrese el apellido del cliente:[/]");
            string cedula = AnsiConsole.Ask<string>("[green]Ingrese la cédula del cliente [white]no -giones-[/]:[/]");
            string telefono = AnsiConsole.Ask<string>("[green]Ingrese el teléfono del cliente [white]no -giones-[/]:[/]");
            int id = ultimoId++;
            AnsiConsole.MarkupLine("[green]Agregando cliente[/]\n");
            AnsiConsole.Status()
                .Start("...", ctx =>
                {
                    Thread.Sleep(3000);
                });
            Cliente nuevoCliente = new Cliente
            {
                Id = id,
                Nombre = nombre,
                Apellido = apellido,
                Cedula = cedula,
                Telefono = telefono
            };
            clientes.Add(nuevoCliente);

            AnsiConsole.MarkupLine("[green]Cliente registrado exitosamente.[/]\n");
            return nuevoCliente;

        }

        public Cliente? BuscarClientePorId(int id)
        {
            return clientes.Find(c => c.Id == id);
        }
        public List<Cliente> GetClientes()
        {
            return clientes;
        }

        public void EliminarCliente()
        {
            int id = AnsiConsole.Ask<int>("[yellow]Que cliente desea eliminar[white] pro el ID[/][/]\n");
            if (AnsiConsole.Confirm("Esta de acuerdo de eliminar\n")) 
            {

                var cliente = clientes.Find(c => c.Id == id);
                if (cliente != null)
                {
                    AnsiConsole.MarkupLine("[red]Eliminando cliente[/]\n");
                    AnsiConsole.Status()
                        .Start("...", ctx =>
                        {
                            Thread.Sleep(3000);
                        });
                    clientes.Remove(cliente);
                    AnsiConsole.MarkupLine($"[green]Cliente eliminado exitosamente.[/]\n");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]No se encontró un cliente con el ID {id}.[/]\n");
                }

            }
            else
            {
                return;
            }
            

        }

    }
}
