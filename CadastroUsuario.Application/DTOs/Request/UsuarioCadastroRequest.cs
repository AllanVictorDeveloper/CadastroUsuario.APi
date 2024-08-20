using System.ComponentModel.DataAnnotations;

namespace CadastroUsuario.Application.DTOs.Request;

public class UsuarioCadastroRequest
{
    [Required(ErrorMessage = "O {0} é obrigatório")]
    public string Name { get; set; }


    [Required(ErrorMessage = "Senha é obrigatório")]
    [DataType(DataType.Password)]
    [StringLength(20, ErrorMessage = "A senha deve conter minimo de {2} caracteres, " +
        "e maximo de {1} caracteres.", MinimumLength = 10)]
    public string Password { get; set; }

    [Required(ErrorMessage = "O {0} é obrigatório")]
    public string Profile { get; set; }

    
}