using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Interfaces.Common;

namespace ANTIL.Domain.Dao.Interfaces
{
    public interface IUserDao : IGenericDao<User>
    {
        bool IsExistUser(string login, string password);
    }
}
