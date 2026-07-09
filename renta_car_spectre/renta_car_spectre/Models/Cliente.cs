using System;
using System.Collections.Generic;
using System.Text;

namespace renta_car_spectre.Models
{
    internal class Cliente
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Cedula { get; set; }
        public required string Telefono { get; set; }
    }
}
    