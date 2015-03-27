using System.Collections.Generic;
using ANTIL.Domain.Core.Entities.Common;

namespace ANTIL.Domain.Core.Entities
{
    public class Commit : DomainObject
    {
        public virtual string Name { get; set; }

        public virtual Commit ParentCommit { get; set; }
        
        public virtual Project Project { get; set; }

        public virtual IList<AntilFile> Files { get; set; }

        public virtual string Comment { get; set; }

        public Commit()
        {
            Files = new List<AntilFile>();
        }
    }
}
