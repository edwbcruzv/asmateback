using Ardalis.Specification;
using Domain.Entities;
using System.Linq;

namespace Application.Specifications.Users
{
    public class UserByNickNameAndNotIdSpecification : Specification<User>
    {
        public UserByNickNameAndNotIdSpecification(string nickName, int Id)
        {
            Query.Where(x => x.NickName.Equals(nickName) && x.Id != Id);
        }
    }
}
