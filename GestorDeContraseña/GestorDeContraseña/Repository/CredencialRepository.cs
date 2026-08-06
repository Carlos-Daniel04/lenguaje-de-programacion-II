using GestorDeContraseña.Database;
using GestorDeContraseña.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GestorDeContraseña.Repository
{
    internal class CredencialRepository
    {

        private readonly AppDbContext _context;

        public CredencialRepository(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<List<Credencial>> GetAllAsync()
        {
            return await _context.Credenciales.AsNoTracking().ToListAsync();
        }

        public async Task<Credencial?> GetByIdAsync(int id)
        {
            return await _context.Credenciales.FindAsync(id);
        }

        public async Task AddAsync(Credencial credential)
        {
            await _context.Credenciales.AddAsync(credential);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Credencial credential)
        {
            _context.Credenciales.Update(credential);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Credenciales.FindAsync(id);
            if (item != null)
            {
                _context.Credenciales.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
