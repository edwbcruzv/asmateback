using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum EstadoTransaccion
    {
        [Description("NO EXITOSO")]
        NoExitoso,

        [Description("EXITOSO")]
        Exitoso,

        [Description("CONDONACIÓN DE INTERESES")]
        CondonacionDeIntereses,

        [Description("RETIRO VOLUNTARIO 20%")]
        RetiroVoluntario20,

        [Description("RETIRO VOLUNTARIO 50%")]
        RetiroVoluntario50,

        [Description("RETIRO VOLUNTARIO 100%")]
        RetiroVoluntario100
    }
}
