using Dapper;
using System.Data;
using VehiculoMVC.Models.Entities;

namespace VehiculoMVC.Repositories
{
    public class AuthRepository
    {
        private readonly IDbConnection _context;

        public AuthRepository(IDbConnection context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerUsuario(string username)
        {
            var query = "SELECT Id, Username, PasswordHash, Rol FROM Usuarios WHERE Username = @Username";
            return await _context.QueryFirstOrDefaultAsync<Usuario?>(query, new { Username = username });
        }
    }
}