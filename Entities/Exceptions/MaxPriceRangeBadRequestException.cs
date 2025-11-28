using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class MaxPriceRangeBadRequestException : BadRequestException
    {
        public MaxPriceRangeBadRequestException() : base("Max price can't be less than min price.") { }
    }
}
