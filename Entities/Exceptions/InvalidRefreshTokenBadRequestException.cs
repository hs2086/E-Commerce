using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class InvalidRefreshTokenBadRequestException : BadRequestException
    {
        public InvalidRefreshTokenBadRequestException(string message) : base(message)
        {
        }
    }
}
