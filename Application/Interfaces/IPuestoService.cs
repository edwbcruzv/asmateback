using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPuestoService
    {
        public string GenerateClave(string company_name, string puesto_name, int puesto_id);
    }
}
