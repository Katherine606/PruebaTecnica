using Microsoft.AspNetCore.Mvc;
using MVCTecnica.Models.ViewModels;
using MVCTecnica.Services;

namespace MVCTecnica.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly VehiculoService _vehiculoService;

        public VehiculoController(VehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        public async Task<IActionResult> Index()
        {
           
            var vehiculos = await _vehiculoService.ObtenerTodosAsync();

            var model = vehiculos.Select(v => new VehiculoListaViewModel
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

          
            return View(model);
        }
    }
}