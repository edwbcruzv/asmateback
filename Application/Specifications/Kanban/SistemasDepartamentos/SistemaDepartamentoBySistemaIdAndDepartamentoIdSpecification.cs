using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Kanban.SistemasDepartamentos
{
    public class SistemaDepartamentoBySistemaIdAndDepartamentoIdSpecification : Specification<SistemaDepartamento>
    {
        public SistemaDepartamentoBySistemaIdAndDepartamentoIdSpecification(int sistema_id, int departamento_id)
        {
            Query.Where(x => x.SistemaId == sistema_id && x.DepartamentoId == departamento_id);
        }
    }
}
