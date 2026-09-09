using Microsoft.AspNetCore.Mvc;
using VehiculoMVC.Exceptions;
using VehiculoMVC.Models.ViewModels.Auth;
using VehiculoMVC.Services;

namespace VehiculoMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
       
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(AuthLoginVM modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            try
            {
                var token = await _authService.IniciarSesion(modelo);

                if (string.IsNullOrEmpty(token))
                {
                    ViewBag.Error = "No se pudo iniciar sesion.";
                    return View(modelo);
                }

                HttpContext.Session.SetString("Token", token);

                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var rol = jwtToken.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role || c.Type == "role")?.Value;
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier || c.Type == "sub")?.Value;

                if (!string.IsNullOrEmpty(rol)) HttpContext.Session.SetString("Rol", rol);
                if (!string.IsNullOrEmpty(userId)) HttpContext.Session.SetString("UserId", userId);

                return RedirectToAction("Index", "Vehiculos");
            }
            catch (ApiException ex) // <-- Captura tu excepción personalizada de negocio
            {
                ViewBag.Error = ex.Message; // Muestra "El usuario no existe" o "La contraseña es incorrecta"
                return View(modelo);
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado en el servidor.";
                return View(modelo);
            }
        }

        
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }


    }
}
