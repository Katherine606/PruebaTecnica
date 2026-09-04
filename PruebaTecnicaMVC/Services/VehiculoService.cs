
using PruebaTecnicaMVC.Exceptions;
using PruebaTecnicaMVC.Repositories;
using PruebaTecnicaMVC.Models.DTOs.Vehiculo;
using PruebaTecnicaMVC.Models.Entities;

namespace PruebaTecnicaMVC.Services
{
    public class VehiculoService
    {
        private readonly VehiculoRepository _vehiculoRepository;

        public VehiculoService(VehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }

        public async Task<IEnumerable<VehiculoRespuestaDto>> ObtenerTodosAsync() { 
              
                
           var vehiculos = await _vehiculoRepository.ObtenerTodosAsync();
            
            return vehiculos.Select(v => new VehiculoRespuestaDto
            {
                Id = v.Id,
                Placa = v.Placa,
                Marca = v.Marca,
                Modelo = v.Modelo,
                AnioFabricacion = v.AnioFabricacion,
                PrecioAlquilerPorDia = v.PrecioAlquilerPorDia,
                Kilometraje = v.Kilometraje,
                Categoria = v.Categoria,
                Estado = v.Estado
            });
        } 

        //public async Task<Vehiculo> ObtenerPorIdAsync(int id)
        //{
        //    var vehiculo = await _vehiculoRepository.ObtenerPorIdAsync(id);
        //    if (vehiculo == null) throw new ApiException("Vehículo no encontrado", 404);
        //    return vehiculo;
        //}

        //public async Task CrearAsync(VehiculoCrearDto dto)
        //{
        //    var existePlaca = await _vehiculoRepository.ObtenerPorPlacaAsync(dto.Placa);
        //    if (existePlaca != null)
        //    {
            
        //        if (existePlaca.EstadoLogico == "N")
        //            throw new ApiException("Ya existe un vehículo con esta placa pero se encuentra eliminado.", 400);

        //        throw new ApiException("Ya existe un vehículo registrado con esta placa.", 400);
        //    }

        //    if (dto.AnioFabricacion > DateTime.Now.Year)
        //        throw new ApiException("El año de fabricación no puede ser futuro.", 400);

        //    await _vehiculoRepository.CrearAsync(dto);
        //}

        //public async Task ActualizarAsync(int id, VehiculoCrearDto dto)
        //{
        //    await ObtenerPorIdAsync(id);

        //    var vehiculoPlaca = await _vehiculoRepository.ObtenerPorPlacaAsync(dto.Placa);
        //    if (vehiculoPlaca != null && vehiculoPlaca.Id != id)
        //        throw new ApiException("La placa ingresada ya pertenece a otro vehículo.", 400);

        //    if (dto.AnioFabricacion > DateTime.Now.Year)
        //        throw new ApiException("El año de fabricación no puede ser futuro.", 400);

        //    await _vehiculoRepository.ActualizarAsync(id, dto);
        //}

        //public async Task EliminarAsync(int id)
        //{
        //    await ObtenerPorIdAsync(id);
        //    await _vehiculoRepository.EliminarAsync(id);
        //}

        //public async Task<TableroRespuestaDto> ObtenerTableroReporteAsync()
        //{
        //    var vehiculos = await _vehiculoRepository.ObtenerTodosAsync();

        //    var detalleFlota = vehiculos.Select(v => new VehiculoRespuestaDto
        //    {
        //        Id = v.Id,
        //        Placa = v.Placa,
        //        Marca = v.Marca,
        //        Modelo = v.Modelo,
        //        AnioFabricacion = v.AnioFabricacion,
        //        PrecioAlquilerPorDia = v.PrecioAlquilerPorDia,
        //        Kilometraje = v.Kilometraje,
        //        Categoria = v.Categoria,
        //        Estado = v.Estado,
        //        EstadoLogico = v.EstadoLogico,
        //        ClasificacionKilometraje = v.Kilometraje > 15000 ? "Crítico" : (v.Kilometraje >= 8000 ? "Advertencia" : "Óptimo"),
        //        RecargoSeguroLujo = v.PrecioAlquilerPorDia > 100 ? v.PrecioAlquilerPorDia * 0.12m : 0m
        //    }).ToList();

        //    var desglosePorCategoria = detalleFlota
        //        .GroupBy(v => v.Categoria)
        //        .Select(g =>
        //        {
        //            var valorTotalCategoria = g.Sum(v => v.PrecioAlquilerPorDia);
        //            bool aplicaSeguroLujo = g.Any(v => v.PrecioAlquilerPorDia > 100);
        //            decimal recargoCategoria = aplicaSeguroLujo ? valorTotalCategoria * 0.12m : 0m;

        //            return new CategoriaDesgloseDto
        //            {
        //                Categoria = g.Key,
        //                TotalVehiculos = g.Count(),
        //                PromedioKilometraje = g.Average(v => v.Kilometraje),
        //                CostoPromedioAlquiler = g.Average(v => v.PrecioAlquilerPorDia),
        //                ValorTotalProyectadoDiario = valorTotalCategoria,
        //                TotalRecargosSeguroLujo = recargoCategoria
        //            };
        //        }).ToList();

        //    var atencionInmediata = detalleFlota
        //        .Where(v => v.Kilometraje > 15000 || v.Estado == "En Mantenimiento")
        //        .ToList();

        //    return new TableroRespuestaDto
        //    {
        //        TotalVehiculos = vehiculos.Count(),
        //        ValorMonetarioTotalProyectadoDiario = vehiculos.Sum(v => v.PrecioAlquilerPorDia),
        //        DesglosePorCategoria = desglosePorCategoria,
        //        VehiculosRequierenAtencion = atencionInmediata,
        //        DetalleFlota = detalleFlota
        //    };
        //}
    }
}