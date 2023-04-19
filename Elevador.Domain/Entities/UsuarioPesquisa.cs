using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevador.Domain.Enums;

namespace Elevador.Domain.Entities
{
    public class UsuarioPesquisa
    {
        public int Andar { get; set; }
        public char Elevador { get; set; }
        public char Turno { get; set; }
    }
}
