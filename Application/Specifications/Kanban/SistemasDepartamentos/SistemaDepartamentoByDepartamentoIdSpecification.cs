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
    public class SistemaDepartamentoByDepartamentoIdSpecification : Specification<SistemaDepartamento>
    {
        public SistemaDepartamentoByDepartamentoIdSpecification( int departamento_id)
        {
            Query.Where(x => x.DepartamentoId == departamento_id);
        }
    }
}
