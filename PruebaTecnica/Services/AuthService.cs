using Microsoft.IdentityModel.Tokens;
using PruebaTecnica.DTOs.Auth;
using PruebaTecnica.Exceptions;
using PruebaTecnica.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaTecnica.Services
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

        //crear usuario para hashear
        public async Task CrearUsuarioAsyn(LoginDto dto)
        {

            //hasheo
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await _authRepository.CrearUsuario(dto.Username, passwordHash);

        }

        //login para verificar si el usuario existe y si la contraseña es correcta
        public async Task<string> LoginAsync(LoginDto dto)
        {
            var usuario = await _authRepository.ObtenerUsername(dto.Username);
            if (usuario == null)
            {
                throw new ApiException("Usuario o contraseña incorrectos", 400);
            }
            var passwordCorrecta = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);
            if (!passwordCorrecta)
            {
                throw new ApiException("Usuario o contraseña incorrectos", 401);
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
