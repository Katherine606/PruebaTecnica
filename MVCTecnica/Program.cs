using System.Data;
using Microsoft.Data.SqlClient;
using MVCTecnica.Repositories; // Ajusta según tu espacio de nombres real
using MVCTecnica.Services;    // Ajusta según tu espacio de nombres real

var builder = WebApplication.CreateBuilder(args);

// 1. Obtener la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("connectionDB");

// 2. Registrar la conexión para Dapper / IDbConnection
builder.Services.AddTransient<IDbConnection>(sp => new SqlConnection(connectionString));

// 3. REGISTRAR EL REPOSITORIO Y EL SERVICIO (Esto resuelve el error)
builder.Services.AddScoped<VehiculoRepository>();
builder.Services.AddScoped<VehiculoService>();

// Configuración adicional (AddControllersWithViews, etc.)
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
