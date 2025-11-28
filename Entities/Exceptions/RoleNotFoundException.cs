using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(string message) : base(message)
        {
        }
    }
}
