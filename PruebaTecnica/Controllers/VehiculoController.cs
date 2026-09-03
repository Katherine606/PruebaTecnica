using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTOs.Vehiculo;
using PruebaTecnica.Services;

namespace PruebaTecnica.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly VehiculoService _vehiculoService;

        public VehiculosController(VehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet("Listar")]
       
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var lista = await _vehiculoService.ObtenerTodosAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var vehiculo = await _vehiculoService.ObtenerPorIdAsync(id);
            return Ok(vehiculo);
        }

        [HttpGet("tablero-reporte")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> ObtenerTableroReporte()
        {
            var reporte = await _vehiculoService.ObtenerTableroReporteAsync();
            return Ok(reporte);
        }

        [HttpPost("Crear")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] VehiculoCrearDto dto)
        {
            await _vehiculoService.CrearAsync(dto);
            return Ok(new { mensaje = "Vehículo creado exitosamente." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] VehiculoCrearDto dto)
        {
            await _vehiculoService.ActualizarAsync(id, dto);
            return Ok(new { mensaje = "Vehículo actualizado exitosamente." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _vehiculoService.EliminarAsync(id);
            return Ok(new { mensaje = "Vehículo eliminado lógicamente de forma exitosa." });
        }
    }
}