using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnicaMVC.Middleware;
using PruebaTecnicaMVC.Repositories;
using PruebaTecnicaMVC.Services;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Dapper
builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("connectionDB")));

// Soporte para Vistas MVC y Controladores API
builder.Services.AddControllersWithViews();


// Repositorios y Servicios


builder.Services.AddScoped<VehiculoRepository>();
builder.Services.AddScoped<VehiculoService>();



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseMiddleware<ExcepcionesGlobales>();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba Tecnica API v1");
//        c.RoutePrefix = "swagger"; // Mantiene Swagger en /swagger para no adueñarse de la raíz
//    });
//}
app.UseHttpsRedirection();
app.UseRouting();

//app.UseSession(); // Se ejecuta antes de Auth

//app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
// 1. Mapea endpoints de la API (vía atributos [ApiController])
app.MapControllers();

// 2. Mapea la ruta por defecto para MVC Razor
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();