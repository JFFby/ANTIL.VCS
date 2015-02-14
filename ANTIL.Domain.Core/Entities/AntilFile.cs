using System;
using ANTIL.Domain.Core.Entities.Common;

namespace ANTIL.Domain.Core.Entities
{
    public class AntilFile : EditableDomainObject
    {
        public virtual string Name { get; set; }

        public virtual string Path { get; set; }

        public virtual string Extension { get; set; }

        public virtual string CommitName { get; set; }

        public virtual string Project { get; set; }

        public virtual string Owner { get; set; }

        public virtual string ParentCommit { get; set; }

        public virtual byte[] Data { get; set; }

    }
}
