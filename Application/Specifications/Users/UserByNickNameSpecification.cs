using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Users
{
    public class UserByNickNameSpecification : Specification<User>
    {
        public UserByNickNameSpecification(string nickName)
        {
            Query.Where(x => x.NickName.Equals(nickName));
        }
    }
}
