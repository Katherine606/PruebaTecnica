using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMVC.Models;
using PruebaTecnicaMVC.Services;

namespace PruebaTecnicaMVC.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly ApiService _apiService;

        public VehiculosController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _apiService.ObtenerVehiculosAsync();
            return View(lista);
        }

        public async Task<IActionResult> Tablero()
        {
            var tablero = await _apiService.ObtenerTableroAsync();
            return View(tablero);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(VehiculoViewModel modelo)
        {
            if (!ModelState.IsValid) return View(modelo);

            var resultado = await _apiService.CrearVehiculoAsync(modelo);
            if (resultado) return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Error al registrar el vehículo en la API.");
            return View(modelo);
        }

    }
}