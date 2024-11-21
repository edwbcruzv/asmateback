using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum EstatusOperacion
    {
        [Description("Pendiente")]
        Pendiente,
        [Description("Autorizado")]
        Activo,
        [Description("Rechazado")]
        Rechazado,
        [Description("Finiquitado")]
        Finiquitado
    }
}
