using System;

namespace ANTIL.Domain.Core.Entities.Common
{
    public class EditableDomainObject : DomainObject
    {
        public virtual DateTime Updated { get; set; }

        public virtual int Version { get; set; }
    }
}
