using Dapper;
using PruebaTecnicaMVC.Models.Entities;
using System.Data;

namespace PruebaTecnica.Repositories
{
    public class AuthRepository
    {
        private readonly IDbConnection _context;

        public AuthRepository(IDbConnection context)
        {
            _context = context;
        }


        public async Task CrearUsuario(string username, string password)
        {
            
            var query = "INSERT INTO Usuarios (Username, PasswordHash, Rol) VALUES (@Username, @PasswordHash, 'User')";

            await _context.ExecuteAsync(query, new { Username = username, PasswordHash = password });

        }


        public async Task<Usuario?> ObtenerUsername(string username)
        {

            var query = "SELECT Id, Username, PasswordHash, Rol FROM Usuarios WHERE Username = @Username";
            return await _context.QueryFirstOrDefaultAsync<Usuario>(query, new { Username = username });

        }


    }
}
