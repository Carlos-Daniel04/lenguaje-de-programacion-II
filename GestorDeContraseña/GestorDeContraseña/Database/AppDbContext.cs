using GestorDeContraseña.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GestorDeContraseña.Database
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Credencial> Credenciales { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=passwords.db");
        }
    }
}
