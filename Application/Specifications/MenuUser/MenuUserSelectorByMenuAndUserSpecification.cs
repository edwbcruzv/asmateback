using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.MenuUser
{
    public class MenuUserSelectorByMenuAndUserSpecification : Specification<MenuUserSelector>
    {
        public MenuUserSelectorByMenuAndUserSpecification(int menu, int user)
        {
            Query.Where(x => x.UserId == user && x.MenuId == menu);
        }
    }
}
