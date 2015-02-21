using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Implementations.Common;
using ANTIL.Domain.Dao.Interfaces;
using ANTIL.Domain.Dao.Interfaces.Common;

namespace ANTIL.Domain.Dao.Implementations
{
    public class ProjectDao : GenericDao<Project>, IProjectDao
    {
        public ProjectDao(IDataAccessObject obj) : base(obj) { }
    }
}
