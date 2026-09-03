using PruebaTecnicaMVC.Models;
using PruebaTecnicaMVC.Models.Auth;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PruebaTecnicaMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        private void AdjuntarToken()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<AuthLoginResponse?> LoginAsync(AuthLoginVM modelo)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", modelo);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Credenciales inválidas o error en el servidor.");
            }

            return await response.Content.ReadFromJsonAsync<AuthLoginResponse>();
        }

        public async Task<List<VehiculoViewModel>> ObtenerVehiculosAsync()
        {
            AdjuntarToken();
            var response = await _httpClient.GetAsync("api/Vehiculos/listar");

            if (!response.IsSuccessStatusCode)
            {
                return new List<VehiculoViewModel>();
            }

            var json = await response.Content.ReadAsStringAsync();

      
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<VehiculoViewModel>>(json, options) ?? new List<VehiculoViewModel>();
        }

        public async Task<TableroViewModel?> ObtenerTableroAsync()
        {
            AdjuntarToken();
            var response = await _httpClient.GetAsync("api/Vehiculos/tablero-reporte");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TableroViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> CrearVehiculoAsync(VehiculoViewModel modelo)
        {
            AdjuntarToken();
            var content = new StringContent(JsonSerializer.Serialize(modelo), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Vehiculos", content);
            return response.IsSuccessStatusCode;
        }

    }
}