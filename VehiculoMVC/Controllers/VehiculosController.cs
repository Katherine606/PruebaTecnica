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
        public IActionResult Crear()
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
                        ViewBag.message = ex.Message; 
                        return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var vm = await _vehiculoService.ObtenerParaEditarAsync(id);
                return View(vm);
            }
            catch (ApiException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VehiculoEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                await _vehiculoService.ActualizarAsync(vm);
                return RedirectToAction("Index");
            }
            catch (ApiException ex)
            {
                ViewBag.message = ex.Message;
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(string placa)
        {
            try
            {
                await _vehiculoService.EliminarAsync(placa);
                return RedirectToAction("Index");
            }
            catch (ApiException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Tablero()
        {
            var tablero = await _vehiculoService.ObtenerTableroReporteAsync();
            return View(tablero);
        }
    }
}