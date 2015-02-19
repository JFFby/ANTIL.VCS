using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandHandler.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowUnauthorized: Attribute
    {
    }
}
