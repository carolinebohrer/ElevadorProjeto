using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevador.Domain.Enums
{
    public enum EnumTurno
    {
        [Description("M")]
        Matutino = 1,

        [Description("V")]
        Vespertino = 2,

        [Description("N")]
        Noturno = 3
    }
}
