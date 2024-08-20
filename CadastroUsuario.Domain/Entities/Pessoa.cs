using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuario.Domain.Entities
{
    public class Pessoa : EntityBase
    {
        public string? Nome { get; set; }
        public string? SobreNome { get; set; }
        public string? CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        public string? Sexo { get; set; }

        public string? Foto { get; set; }

        public Pessoa(string? nome, string? sobreNome, string? cPF, DateTime dataNascimento, string? sexo, string? foto)
        {
            Nome = nome;
            SobreNome = sobreNome;
            CPF = cPF;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Foto = foto;
            DataCadastro = DateTime.Now;
        }

        public void Update(string? nome, string? sobreNome, string? cPF, DateTime dataNascimento, string? sexo, string? foto)
        {
            this.Nome = nome;
            SobreNome = sobreNome;
            CPF = cPF;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Foto = foto;
        }
    }
}
