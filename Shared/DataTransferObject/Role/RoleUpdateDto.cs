using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Role
{
    public class RoleUpdateDto
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
