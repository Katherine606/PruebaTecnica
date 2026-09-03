using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMVC.Models.Auth;
using PruebaTecnicaMVC.Services;


namespace PruebaTecnicaMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AuthLoginVM modelo)
        {
            try
            {
                var resultado = await _apiService.LoginAsync(modelo);
                if (resultado != null && !string.IsNullOrEmpty(resultado.Token))
                {
                    
                    HttpContext.Session.SetString("JWToken", resultado.Token);
                    return RedirectToAction("Index", "Vehiculos");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Credenciales incorrectas.");
            }
            return View(modelo);
        }
    }

}




