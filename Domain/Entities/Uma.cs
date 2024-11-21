﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Uma : AuditableBaseEntity
    {
        public int Anio { get; set; }
        public double Monto { get; set; }
    }
}
