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

        public ICollection<Foto> Fotos { get; set; } = new List<Foto>();

        
    }
}
