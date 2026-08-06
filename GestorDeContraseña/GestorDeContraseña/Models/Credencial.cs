using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeContraseña.Models
{
    internal class Credencial
    {
    
        public int Id { get; set; }
        public string SiteName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

