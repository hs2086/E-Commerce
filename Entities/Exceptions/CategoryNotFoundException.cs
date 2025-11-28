using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(int id) : base($"The category with id: {id} doesn't exist in the database.") { }
        public CategoryNotFoundException(string message) : base(message) { }
    }
}
