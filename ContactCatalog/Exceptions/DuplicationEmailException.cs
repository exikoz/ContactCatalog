using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Exceptions
{
    public class DuplicationEmailException : Exception
    {
        public string Email { get; }

        public DuplicationEmailException(string email) : base($"Email already exist: '{email}'")
        {
            Email = email;
        }
    }
}
