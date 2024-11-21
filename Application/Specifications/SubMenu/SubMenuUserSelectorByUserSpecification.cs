using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.SubMenu
{
    public class SubMenuUserSelectorByUserSpecification : Specification<SubMenuUserSelector>
    {
        public SubMenuUserSelectorByUserSpecification(int id)
        {
            Query.Where(x => x.UserId == id);
        }
    }
}
