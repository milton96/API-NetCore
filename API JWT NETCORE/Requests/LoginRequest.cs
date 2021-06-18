using API_JWT_NETCORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_JWT_NETCORE.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El correo es requerido")]
        [RegularExpression(RegexHelper.Correo, ErrorMessage = "El correo no tiene el formato correcto")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
    }
}
