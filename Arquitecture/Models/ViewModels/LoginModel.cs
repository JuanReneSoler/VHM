using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels;
public class LoginModel
{
    [Required(ErrorMessage = "Usuario requerido")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    public string Password { get; set; }

    public bool IsPersistent { get; set; }
}
