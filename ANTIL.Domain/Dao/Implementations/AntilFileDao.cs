using ANTIL.Domain.Dao.Implementations.Common;
using ANTIL.Domain.Dao.Interfaces;
using ANTIL.Domain.Dao.Interfaces.Common;
using ANTIL.Domain.Entities;

namespace ANTIL.Domain.Dao.Implementations
{
    public class AntilFileDao : GenericDao<AntilFile>, IAntilFileDao
    {
        public AntilFileDao(IDataAccessObject obj) : base(obj) { }
    }
}
