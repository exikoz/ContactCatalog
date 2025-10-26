using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        IEnumerable<Contact> GetAll();
        Contact? GetById(int id);
    }
}
