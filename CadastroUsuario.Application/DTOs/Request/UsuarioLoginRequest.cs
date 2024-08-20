using System.ComponentModel.DataAnnotations;

namespace CadastroUsuario.Application.DTOs.Request;

public class UsuarioLoginRequest
{
    [Required(ErrorMessage = "O {0} é obrigatório")]
    //[EmailAddress(ErrorMessage = "Email é inválido")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O {0} é obrigatório")]
    public string Password { get; set; }
}