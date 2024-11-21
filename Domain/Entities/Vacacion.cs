using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Vacacion : AuditableBaseEntity
    {
        public int AnioLimiteInferior { get; set; }
        public int AnioLimiteSuperior { get; set; }
        public int Dias { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

    }
}
