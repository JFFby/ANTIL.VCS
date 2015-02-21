using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Interfaces.Common;

namespace ANTIL.Domain.Dao.Interfaces
{
    public interface ICommitDao : IGenericDao<Commit>
    {
        bool IsUniqueCommit(string name, Project proj);

        Commit Get(string name, Project proj);
    }
}
