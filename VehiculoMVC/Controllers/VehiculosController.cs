using Microsoft.AspNetCore.Mvc;
using VehiculoMVC.Exceptions;
using VehiculoMVC.Models.DTOs.Vehiculo;
using VehiculoMVC.Models.ViewModels;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehiculosVm = await _vehiculoService.ObtenerTodosAsync();
            return View(vehiculosVm);
        }


        //crear vehiculo
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(VehiculoCrearVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                await _vehiculoService.CrearAsync(vm);
                return RedirectToAction("Index");
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        //public async Task<IActionResult> Tablero()
        //{
        //    var tablero = await _vehiculoService.ObtenerTableroReporteAsync();
        //    return View(tablero);
        //}
    }
}