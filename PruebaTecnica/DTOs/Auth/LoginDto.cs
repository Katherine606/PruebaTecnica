using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.DTOs.Auth
{
    public class LoginDto
    {
        [Required (ErrorMessage = "El campo username es obligatorio")]
        public  string Username { get; set; }
        [Required(ErrorMessage = "El campo password es obligatorio")]
        public  string Password { get; set; }
    }
}
