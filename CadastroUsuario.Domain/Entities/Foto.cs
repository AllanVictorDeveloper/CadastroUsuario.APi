using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuario.Domain.Entities
{
    public class Foto : EntityBase
    {
        public byte[] Imagem { get; set; } // Armazena a imagem como um blob
        public string NomeHash { get; set; } // Nome hash do arquivo
        public string Extensao { get; set; } // Extensão do arquivo
        public bool Principal { get; set; } // Flag para indicar se é a foto principal
        public int PessoaId { get; set; } // Chave estrangeira para Pessoa
        public virtual Pessoa Pessoa { get; set; } // Navegação
    }

}
