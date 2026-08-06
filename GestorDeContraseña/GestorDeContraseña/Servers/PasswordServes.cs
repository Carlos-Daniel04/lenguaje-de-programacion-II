using GestorDeContraseña.Models;
using GestorDeContraseña.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GestorDeContraseña.Servers
{
    internal class PasswordServers
    {
        private readonly CredencialRepository _repository;

        public PasswordServers(CredencialRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Credencial>> GetCredentialsAsync() => _repository.GetAllAsync();

        public Task<Credencial?> GetCredentialByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task CreateCredencialAsync(string siteName, string username, string password)
        {
            var credential = new Credencial
            {
                SiteName = siteName,
                Username = username,
                Password = password
            };
            return _repository.AddAsync(credential);
        }

        public async Task<bool> UpdateCredentialAsync(int id, string siteName, string username, string password)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.SiteName = siteName;
            existing.Username = username;
            existing.Password = password;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteCredentialAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
