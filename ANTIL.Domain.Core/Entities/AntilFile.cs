using System;
using ANTIL.Domain.Core.Entities.Common;

namespace ANTIL.Domain.Core.Entities
{
    public class AntilFile : EditableDomainObject
    {
        public virtual string Name { get; set; }

        public virtual string Path { get; set; }

        public virtual string Extension { get; set; }

        public virtual byte[] Data { get; set; }

        public virtual Commit Commit { get; set; }

        public virtual int? Version { get; set; }

        public virtual string Status { get; set; }

    }
}
