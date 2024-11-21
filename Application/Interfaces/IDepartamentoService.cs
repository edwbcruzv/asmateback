using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDepartamentoService
    {
        public string GenerateClave(string company_name, string depto_name, int depto_id);
    }
}
