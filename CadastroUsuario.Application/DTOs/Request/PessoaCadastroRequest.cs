

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CadastroUsuario.Application.DTOs.Request
{
    public class PessoaCadastroRequest
    {
        public string? Nome { get; set; }
        public string? SobreNome { get; set; }
        public string? CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        public string? Sexo { get; set; }

        [Required]
        public IFormFile Foto { get; set; }
    }
}
