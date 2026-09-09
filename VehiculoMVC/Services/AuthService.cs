using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehiculoMVC.Exceptions;
using VehiculoMVC.Models.DTOs.Auth;
using VehiculoMVC.Models.ViewModels.Auth;
using VehiculoMVC.Repositories;

namespace VehiculoMVC.Services
{
    public class AuthService
    {
        private readonly AuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(AuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }   

        public async Task<string?> IniciarSesion(AuthLoginVM dto)
        {
            var usuario = await _authRepository.ObtenerUsuario(dto.Username);

            if (usuario == null)
            {
                throw new ApiException ("El usuario no existe", 404);

            }

            // Agrega esto para ver qué está leyendo exactamente la base de datos
            Console.WriteLine($"Usuario encontrado: [{usuario.Username}] con Hash: [{usuario.PasswordHash}]");
            Console.WriteLine($"Contraseña ingresada: [{dto.Password}]");

            bool esPasswordValido = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);
            Console.WriteLine($"¿Es válido?: {esPasswordValido}");

            if (!esPasswordValido)
            {
                throw new ApiException("La contraseña es incorrecta",400);
            }

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Username),
            new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
