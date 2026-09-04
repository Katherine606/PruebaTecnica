

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTOs.Auth;
using PruebaTecnica.Services;

namespace PracticaJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

  
        //[HttpPost("CrearUsuario")]
        //public async Task<IActionResult> Registrar([FromBody] LoginDto dto)
        //{
           
        //    await _authService.CrearUsuarioAsyn(dto);
        //    return Ok(new { mensaje = "Usuario registrado exitosamente." });
           
        //}

      
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(new { token });
        }
    }
}
