using ANTIL.Domain.Entities.Common;

namespace ANTIL.Domain.Entities
{
    public class AntilFile : EditableDomainObject
    {
        public virtual string Name { get; set; }

        public virtual string Path { get; set; }
    }
}
