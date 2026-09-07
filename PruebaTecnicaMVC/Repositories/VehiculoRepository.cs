using Dapper;
using PruebaTecnicaMVC.Models.DTOs.Vehiculo;
using PruebaTecnicaMVC.Models.Entities;
using System.Data;

namespace PruebaTecnicaMVC.Repositories
{
    public class VehiculoRepository
    {
        private readonly IDbConnection _context;

        public VehiculoRepository(IDbConnection context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehiculo>> ObtenerTodosAsync()
        {
            return await _context.QueryAsync<Vehiculo>("SELECT * FROM Vehiculos WHERE EstadoLogico != 'N'");
        }

      
    }
}