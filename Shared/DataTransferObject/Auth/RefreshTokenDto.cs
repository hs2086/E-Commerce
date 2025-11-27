using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Auth
{
    public class RefreshTokenDto
    {
        public string Email { get; set; }
        public string RefreshToken { get; set; }    
    }
}
