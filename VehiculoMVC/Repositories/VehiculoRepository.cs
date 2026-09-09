using Dapper;
using VehiculoMVC.Models.DTOs.Vehiculo;
using VehiculoMVC.Models.Entities;
using System.Data;

namespace VehiculoMVC.Repositories
{
    public class VehiculoRepository
    {
        private readonly IDbConnection _context;

        public VehiculoRepository(IDbConnection context)
        {
            _context = context;
        }

        //listar todos
        public async Task<IEnumerable<Vehiculo>> ObtenerTodosAsync()
        {
            return await _context.QueryAsync<Vehiculo>("SELECT * FROM Vehiculos WHERE EstadoLogico != 'N'");
        }

        //obtener por id
        public async Task<Vehiculo?> ObtenerPorIdAsync(int id)
        {
            return await _context.QueryFirstOrDefaultAsync<Vehiculo>("SELECT * FROM Vehiculos WHERE Id = @Id AND EstadoLogico != 'N'", new { Id = id });
        }

        //obtener por placa

        public async Task<Vehiculo?> ObtenerPorPlacaAsync(string placa)
        {
            return await _context.QueryFirstOrDefaultAsync<Vehiculo>("SELECT * FROM Vehiculos WHERE Placa = @Placa AND EstadoLogico != 'N'", new { Placa = placa });
        }

        //crear vehiculo
        public async Task CrearAsync(VehiculoCrearDto dto)
        {
            var query = "INSERT INTO Vehiculos (Placa, Marca, Modelo, AnioFabricacion, PrecioAlquilerPorDia, Kilometraje, Categoria, Estado, EstadoLogico) " +
                        "VALUES (@Placa, @Marca, @Modelo, @AnioFabricacion, @PrecioAlquilerPorDia, @Kilometraje, @Categoria, @Estado, 'A')";
            await _context.ExecuteAsync(query, dto);
        }

        public async Task ActualizarAsync(VehiculoCrearDto dto)
        {
            

            var query = "UPDATE Vehiculos SET Marca = @Marca, Modelo = @Modelo, AnioFabricacion = @AnioFabricacion, " +
                        "PrecioAlquilerPorDia = @PrecioAlquilerPorDia, Kilometraje = @Kilometraje, " +
                        "Categoria = @Categoria, Estado = @Estado, EstadoLogico = @EstadoLogico WHERE Placa = @Placa";

            await _context.ExecuteAsync(query, dto);
        }

    }
}