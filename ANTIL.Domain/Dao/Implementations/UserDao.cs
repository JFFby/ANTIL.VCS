using System.Linq;
using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Implementations.Common;
using ANTIL.Domain.Dao.Interfaces;
using ANTIL.Domain.Dao.Interfaces.Common;

namespace ANTIL.Domain.Dao.Implementations
{
    public class UserDao : GenericDao<User>, IUserDao
    {
        public UserDao(IDataAccessObject obj) : base(obj) { }

        public bool IsExistUser(string login, string password)
        {
            return CreateQuery().Any(u => u.UserName == login && u.Password == password);
        }
    }
}
