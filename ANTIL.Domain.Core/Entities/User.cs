using ANTIL.Domain.Core.Entities.Common;

namespace ANTIL.Domain.Core.Entities
{
    public class User : DomainObject
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
    }
}
