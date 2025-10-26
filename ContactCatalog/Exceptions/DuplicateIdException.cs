using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Exceptions
{
    public class DuplicateIdException : Exception
    {
        public int Id { get; }

        public DuplicateIdException(int id)
            : base($"Contact with ID {id} already exists in catalog")
        {
            Id = id;
        }
    }
}
