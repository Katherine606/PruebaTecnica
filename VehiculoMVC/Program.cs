using Microsoft.Data.SqlClient;
using System.Data;
using VehiculoMVC.Repositories;
using VehiculoMVC.Services;

var builder = WebApplication.CreateBuilder(args);
// Dapper
builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("connectionDB")));

// Soporte para Vistas MVC y Controladores API
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<VehiculoRepository>();
builder.Services.AddScoped<VehiculoService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
