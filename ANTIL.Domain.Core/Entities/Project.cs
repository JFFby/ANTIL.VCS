using System.Collections.Generic;
using ANTIL.Domain.Core.Entities.Common;

namespace ANTIL.Domain.Core.Entities
{
    public class Project : DomainObject
    {
        public virtual string Name { get; set; }

        public virtual User Owner { get; set; }

        public virtual IList<Commit> Commits { get; set; }

        public Project()
        {
            Commits = new List<Commit>();
        }
    }
}
