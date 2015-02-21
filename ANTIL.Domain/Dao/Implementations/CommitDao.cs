using System.Linq;
using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Implementations.Common;
using ANTIL.Domain.Dao.Interfaces;
using ANTIL.Domain.Dao.Interfaces.Common;

namespace ANTIL.Domain.Dao.Implementations
{
    public class CommitDao : GenericDao<Commit>, ICommitDao
    {
        public CommitDao(IDataAccessObject obj) : base(obj) { }

        public Commit Get(string name, Project proj)
        {
            return CreateQuery().FirstOrDefault(c => c.Name == name && c.Project.Id == proj.Id);
        }

        public bool IsUniqueCommit(string name, Project proj)
        {
            return !CreateQuery().Any(c => c.Name == name && c.Project.Id == proj.Id);
        }
    }
}
