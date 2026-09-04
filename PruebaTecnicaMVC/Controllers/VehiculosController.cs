using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMVC.Services;

namespace PruebaTecnicaMVC.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly VehiculoService _vehiculoService;

        public VehiculosController(VehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        public async Task<IActionResult> Index()
        {
            var vehiculos = await _vehiculoService.ObtenerTodosAsync();
            return View(vehiculos);
        }

        //public async Task<IActionResult> Tablero()
        //{
        //    var tablero = await _vehiculoService.ObtenerTableroReporteAsync();
        //    return View(tablero);
        //}
    }
}