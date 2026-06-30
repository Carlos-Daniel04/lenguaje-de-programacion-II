
using renta_car_spectre.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Spectre.Console;
using System.Formats.Asn1;

namespace renta_car_spectre.Services
{
    public class VehiculoService
    {

        private List<Vehiculo> vehiculos = new List<Vehiculo>
        {
            new Vehiculo{
                Id = 1,
                Marca = "Hoda",
                Modelo = "Civic",
                Color = "Gris",
                Ano = 2015,
                PrecioPorDia = 1500m
            }
        };

        private int ultimoId = 2;
        public void RegistrarVehiculo()
        {
            string marca = AnsiConsole.Ask<string>("[green]Que marca es el vehiculo?[/]");
            string modelo = AnsiConsole.Ask<string>("[green]Que modelo es?[/]");
            string color = AnsiConsole.Ask<string>("[green]Que color es?[/]");
            int ano = AnsiConsole.Ask<int>("[green]Que año es?[/]");
            decimal precio = AnsiConsole.Ask<decimal>("[green]Que precio tendra?[/]");
            int id = ultimoId++;

            AnsiConsole.MarkupLine("[green]Agregando vehiculo[/]\n");
            AnsiConsole.Status()
                .Start("...", ctx =>
                {
                    Thread.Sleep(3000);
                });
            Vehiculo nuevoVehiculo = new Vehiculo
            {
                Id = id,
                Marca = marca,
                Modelo = modelo,
                Color = color,
                Ano = ano,
                PrecioPorDia = precio
            };
            vehiculos.Add(nuevoVehiculo);

            AnsiConsole.MarkupLine("[green]Vehiculo registrado exitosamente.[/]\n");


        }
        public List<Vehiculo> GetVehiculos()
        {
            return vehiculos;
        }


        public void EliminarVehiculo()
        {
            int id = AnsiConsole.Ask<int>("[yellow]Que vehiculo desea eliminar[/]\n");
            if (AnsiConsole.Confirm("Esta de acuerdo de eliminar\n"))
            {
                var vehiculo = vehiculos.Find(v => v.Id == id);
                if (vehiculo != null)
                {
                    AnsiConsole.MarkupLine("[red]Eliminando vehiculo[/]\n");
                    AnsiConsole.Status()
                        .Start("...", ctx =>
                        {
                            Thread.Sleep(3000);
                        });
                    vehiculos.Remove(vehiculo);
                    AnsiConsole.MarkupLine("[green]Vehiculo eliminado[/]\n");
                }
                else
                {
                    AnsiConsole.MarkupLine("[yellow]no se encontro el vehiculo[/]\n");
                }

            }
            else
            {
                return;
            }
          
        }

        public void RentarVehiculo(int id)
        {
            var vehiculo = vehiculos.Find(v => v.Id == id);
            if (vehiculo != null && vehiculo.Disponible)
            {
                vehiculo.Disponible = false;
                AnsiConsole.MarkupLine($"[Yellow]El vehículo {vehiculo.Marca} {vehiculo.Modelo} ha sido rentado.[/]\n");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]El vehículo no está disponible.[/]\n");
            }
        }

        public void DevolverVehiculo(int id)
        {
            var vehiculo = vehiculos.Find(v => v.Id == id);
            if (vehiculo != null && !vehiculo.Disponible)
            {
                vehiculo.Disponible = true;
                AnsiConsole.MarkupLine($"[yellow]El vehículo {vehiculo.Marca} {vehiculo.Modelo} ha sido devuelto.[/]\n");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]El vehículo ya estaba disponible.[/]\n");
            }
        }


    }
}
        


