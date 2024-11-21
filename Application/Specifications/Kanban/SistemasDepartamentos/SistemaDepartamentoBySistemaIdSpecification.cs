using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Kanban.SistemasDepartamentos
{
    public class SistemaDepartamentoBySistemaIdSpecification : Specification<SistemaDepartamento>
    {
        public SistemaDepartamentoBySistemaIdSpecification(int sistema_id)
        {
            Query.Where(x => x.SistemaId == sistema_id);
        }
    }
}
