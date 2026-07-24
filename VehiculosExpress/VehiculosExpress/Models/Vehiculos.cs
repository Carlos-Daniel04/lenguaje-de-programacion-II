using System;
using System.Collections.Generic;
using System.Text;

namespace VehiculosExpress.Models
{
    internal class Vehiculos
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        public int Anio { get; set; } 

    }
}
