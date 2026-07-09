using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace renta_car_spectre.models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public required string Color { get; set; }
        public required int Ano { get; set; }
        public required decimal PrecioPorDia {get; set;}
        public bool Disponible { get; set; } = true;
        public int? ClienteId { get; set; }
    }
}
