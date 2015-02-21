using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Interfaces.Common;

namespace ANTIL.Domain.Dao.Interfaces
{
    public interface IProjectDao : IGenericDao<Project>
    {
        Project GetOrCreateProject(string projectName, User owner);
    }
}
