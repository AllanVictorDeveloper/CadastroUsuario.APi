using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuario.Domain.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
