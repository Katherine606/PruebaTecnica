using Dapper;
using PruebaTecnicaMVC.Models.DTOs.Vehiculo;
using PruebaTecnicaMVC.Models.Entities;
using System.Data;

namespace PruebaTecnica.Repositories
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

        public async Task<Vehiculo?> ObtenerPorIdAsync(int id)
        {
            return await _context.QueryFirstOrDefaultAsync<Vehiculo>("SELECT * FROM Vehiculos WHERE Id = @Id AND EstadoLogico != 'N'", new { Id = id });
        }

        public async Task<Vehiculo?> ObtenerPorPlacaAsync(string placa)
        {
            return await _context.QueryFirstOrDefaultAsync<Vehiculo>("SELECT * FROM Vehiculos WHERE Placa = @Placa", new { Placa = placa });
        }

        public async Task<int> CrearAsync(VehiculoCrearDto dto)
        {
            var query = @"INSERT INTO Vehiculos (Placa, Marca, Modelo, AnioFabricacion, PrecioAlquilerPorDia, Kilometraje, Categoria, Estado, EstadoLogico) 
                  VALUES (@Placa, @Marca, @Modelo, @AnioFabricacion, @PrecioAlquilerPorDia, @Kilometraje, @Categoria, @Estado, @EstadoLogico);
                  SELECT CAST(SCOPE_IDENTITY() as int);";

            return await _context.ExecuteScalarAsync<int>(query, new
            {
                dto.Placa,
                dto.Marca,
                dto.Modelo,
                dto.AnioFabricacion,
                dto.PrecioAlquilerPorDia,
                dto.Kilometraje,
                dto.Categoria,
                dto.Estado,
                dto.EstadoLogico
            });
        }

        public async Task ActualizarAsync(int id, VehiculoCrearDto dto)
        {
            var query = @"UPDATE Vehiculos SET Placa = @Placa, Marca = @Marca, Modelo = @Modelo, AnioFabricacion = @AnioFabricacion, 
                          PrecioAlquilerPorDia = @PrecioAlquilerPorDia, Kilometraje = @Kilometraje, Categoria = @Categoria, Estado = @Estado, EstadoLogico = @EstadoLogico 
                          WHERE Id = @Id";
            await _context.ExecuteAsync(query, new
            {
                Id = id,
                dto.Placa,
                dto.Marca,
                dto.Modelo,
                dto.AnioFabricacion,
                dto.PrecioAlquilerPorDia,
                dto.Kilometraje,
                dto.Categoria,
                dto.Estado,
                dto.EstadoLogico
            });
        }

        public async Task EliminarAsync(int id)
        {
            var query = "UPDATE Vehiculos SET EstadoLogico = 'N' WHERE Id = @Id";
            await _context.ExecuteAsync(query, new { Id = id });
        }

      
    }
}