using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANTIL.Domain.Entities.Common
{
    public class EditableDomainObject : DomainObject
    {
        public virtual DateTime Updated { get; set; }

        public virtual int Version { get; set; }
    }
}
