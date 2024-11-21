﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoImpuesto : AuditableBaseEntity
    {
        public string Descripcion { get; set; }

        public virtual ICollection<MovimientoReembolso> MovimientosReembolso { get; set; }
    }
}
