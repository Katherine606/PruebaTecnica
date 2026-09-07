using VehiculoMVC.Exceptions;
using System.Net;
using System.Text.Json;

namespace VehiculoMVC.Middleware
{
    public class ExcepcionesGlobales
    {
        private readonly RequestDelegate _next;

        public ExcepcionesGlobales(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    mensaje = ex.Message
                });
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    mensaje = "Ocurrió un error interno en el servidor."
                });
            }
        }
    }
}