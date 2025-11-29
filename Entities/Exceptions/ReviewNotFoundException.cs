using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class ReviewNotFoundException : NotFoundException
    {
        public ReviewNotFoundException(string message) : base(message)
        {
        }
    }
}
