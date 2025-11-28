using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.Category
{
    public abstract record CategoryForManipulationDto
    {
        public string Name { get; set; }    
        public string Description { get; set; }
    }
}
