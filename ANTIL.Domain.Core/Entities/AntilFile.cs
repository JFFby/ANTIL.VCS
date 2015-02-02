using ANTIL.Domain.Core.Entities.Common;

namespace ANTIL.Domain.Core.Entities
{
    public class AntilFile : EditableDomainObject
    {
        public virtual string Name { get; set; }

        public virtual string Path { get; set; }
    }
}
