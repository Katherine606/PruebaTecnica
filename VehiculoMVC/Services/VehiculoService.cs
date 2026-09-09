
using VehiculoMVC.Exceptions;
using VehiculoMVC.Repositories;
using VehiculoMVC.Models.DTOs.Vehiculo;
using VehiculoMVC.Models.Entities;
using VehiculoMVC.Models.ViewModels;

namespace VehiculoMVC.Services
{
    public class VehiculoService
    {
        private readonly VehiculoRepository _vehiculoRepository;

        public VehiculoService(VehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }
        public async Task<IEnumerable<VehiculoViewModel>> ObtenerTodosAsync()
        {
            var vehiculos = await _vehiculoRepository.ObtenerTodosAsync();

            return vehiculos.Select(v => new VehiculoViewModel
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
            }).ToList(); 
        }

        public async Task<Vehiculo> ObtenerPorIdAsync(int id)
        {
            var vehiculo = await _vehiculoRepository.ObtenerPorIdAsync(id);
            if (vehiculo == null) throw new ApiException("Vehículo no encontrado", 404);
            return vehiculo;
        }

        //crear vehiculo
        public async Task CrearAsync(VehiculoCrearVM vm)
        {
       
            var vehiculoPlaca = await _vehiculoRepository.ObtenerPorPlacaAsync(vm.Placa);
            if (vehiculoPlaca != null)
                throw new ApiException("La placa ingresada ya pertenece a otro vehículo.", 400);

            if (vm.AnioFabricacion > DateTime.Now.Year)
                throw new ApiException("El año de fabricación no puede ser futuro.", 400);

            var dto = new VehiculoCrearDto
            {
                Placa = vm.Placa,
                Marca = vm.Marca,
                Modelo = vm.Modelo,
                AnioFabricacion = vm.AnioFabricacion,
                PrecioAlquilerPorDia = vm.PrecioAlquilerPorDia,
                Kilometraje = vm.Kilometraje,
                Categoria = vm.Categoria,
                Estado = vm.Estado,
                EstadoLogico = "A"
            };

        
            await _vehiculoRepository.CrearAsync(dto);
        }

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

        public async Task<TableroViewModel> ObtenerTableroReporteAsync()
        {
            var vehiculos = await _vehiculoRepository.ObtenerTodosAsync();

            var detalleFlota = vehiculos.Select(v => new VehiculoViewModel
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
            }).ToList();

            var desglosePorCategoria = detalleFlota
                .GroupBy(v => v.Categoria)
                .Select(g =>
                {
                    var valorTotalCategoria = g.Sum(v => v.PrecioAlquilerPorDia);
                    bool aplicaSeguroLujo = g.Any(v => v.PrecioAlquilerPorDia > 100);
                    decimal recargoCategoria = aplicaSeguroLujo ? valorTotalCategoria * 0.12m : 0m;

                    return new CategoriaDesgloseViewModel
                    {
                        Categoria = g.Key ?? "Sin Categoría",
                        TotalVehiculos = g.Count(),
                        PromedioKilometraje = g.Average(v => v.Kilometraje),
                        CostoPromedioAlquiler = g.Average(v => v.PrecioAlquilerPorDia),
                        ValorTotalProyectadoDiario = valorTotalCategoria,
                        TotalRecargosSeguroLujo = recargoCategoria
                    };
                }).ToList();

            var atencionInmediata = detalleFlota
                .Where(v => v.Kilometraje > 15000 || v.Estado == "En Mantenimiento")
                .Select(v => new VehiculoAtencionViewModel
                {
                    Placa = v.Placa,
                    Marca = v.Marca,
                    Kilometraje = v.Kilometraje,
                    ClasificacionKilometraje = v.Kilometraje > 15000 ? "Crítico" : "Advertencia",
                    Estado = v.Estado
                }).ToList();

            return new TableroViewModel
            {
                TotalVehiculos = vehiculos.Count(),
                ValorMonetarioTotalProyectadoDiario = vehiculos.Sum(v => v.PrecioAlquilerPorDia),
                DesglosePorCategoria = desglosePorCategoria,
                VehiculosRequierenAtencion = atencionInmediata,
                DetalleFlota = detalleFlota
            };
        }

        public async Task<VehiculoEditVM> ObtenerParaEditarAsync(string placa)
        {
            var v = await _vehiculoRepository.ObtenerPorPlacaAsync(placa);
            if (v == null) throw new ApiException("Vehículo no encontrado", 404);

            return new VehiculoEditVM
            {
                Placa = v.Placa,
                Marca = v.Marca,
                Modelo = v.Modelo,
                AnioFabricacion = v.AnioFabricacion,
                PrecioAlquilerPorDia = v.PrecioAlquilerPorDia,
                Kilometraje = v.Kilometraje,
                Categoria = v.Categoria,
                Estado = v.Estado,
                EstadoLogico = v.EstadoLogico
            };
        }

        public async Task ActualizarAsync(VehiculoEditVM vm)
        {
            var vehiculoExistente = await _vehiculoRepository.ObtenerPorPlacaAsync(vm.Placa);
            if (vehiculoExistente == null)
                throw new ApiException("El vehículo a actualizar no existe.", 404);

            if (vm.AnioFabricacion > DateTime.Now.Year)
                throw new ApiException("El año de fabricación no puede ser futuro.", 400);

            var dto = new VehiculoCrearDto
            {
                Placa = vm.Placa,
                Marca = vm.Marca,
                Modelo = vm.Modelo,
                AnioFabricacion = vm.AnioFabricacion,
                PrecioAlquilerPorDia = vm.PrecioAlquilerPorDia,
                Kilometraje = vm.Kilometraje,
                Categoria = vm.Categoria,
                Estado = vm.Estado,
                EstadoLogico = vm.EstadoLogico
            };

            await _vehiculoRepository.ActualizarAsync(dto);
        }
    }
}