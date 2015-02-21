using System.Linq;
using System.Runtime.InteropServices;
using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Implementations.Common;
using ANTIL.Domain.Dao.Interfaces;
using ANTIL.Domain.Dao.Interfaces.Common;

namespace ANTIL.Domain.Dao.Implementations
{
    public class ProjectDao : GenericDao<Project>, IProjectDao
    {
        public ProjectDao(IDataAccessObject obj) : base(obj) { }

        public Project GetOrCreateProject(string projectName, User owner)
        {
            var proj = CreateQuery().FirstOrDefault(p => p.Name == projectName && p.Owner.Id == owner.Id );
            if (proj == null)
            {
                proj = new Project
                {
                    Name = projectName,
                    Owner = owner
                };
            }

            return proj;
        }
    }
}
