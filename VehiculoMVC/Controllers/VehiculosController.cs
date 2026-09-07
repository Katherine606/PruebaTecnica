using Microsoft.AspNetCore.Mvc;
using VehiculoMVC.Services;

namespace VehiculoMVC.Controllers
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
            var vehiculosVm = await _vehiculoService.ObtenerTodosAsync();
            return View(vehiculosVm);
        }

        //public async Task<IActionResult> Tablero()
        //{
        //    var tablero = await _vehiculoService.ObtenerTableroReporteAsync();
        //    return View(tablero);
        //}
    }
}