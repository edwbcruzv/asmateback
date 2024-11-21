using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.MenuUser
{
    public class MenuUserSelectorByUserSpecification : Specification<MenuUserSelector>
    {
        public MenuUserSelectorByUserSpecification(int id)
        {
            Query.Where(x => x.UserId == id);
        }
    }
}
