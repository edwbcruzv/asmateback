using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Kanban.Sistemas
{
    public class SistemaByEstadoIdSpecification : Specification<Sistema>
    {
        public SistemaByEstadoIdSpecification(int Id)
        {
            Query.Where(x => x.EstadoId == Id);
        }
    }
}
