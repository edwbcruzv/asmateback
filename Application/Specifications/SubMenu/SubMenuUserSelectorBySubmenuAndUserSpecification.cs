using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.SubMenu
{
    public class SubMenuUserSelectorBySubmenuAndUserSpecification : Specification<SubMenuUserSelector>
    {
        public SubMenuUserSelectorBySubmenuAndUserSpecification(int submenu, int user)
        {
            Query.Where(x => x.UserId == user && x.SubMenuId == submenu);
        }
    }
}
