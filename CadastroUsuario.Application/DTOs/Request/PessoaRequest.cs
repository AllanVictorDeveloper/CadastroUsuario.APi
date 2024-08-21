

namespace CadastroUsuario.Application.DTOs.Request
{
    public class PessoaRequest
    {
        public string? Nome { get; set; }
        public string? SobreNome { get; set; }
        public string? CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        public string? Sexo { get; set; }

        public string? Foto { get; set; }
    }
}
