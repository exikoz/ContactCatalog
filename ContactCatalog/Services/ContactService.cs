using ContactCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Services
{
    public class ContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }

        public void AddContact(Contact contact)
        {
            _repository.Add(contact);
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Contact> SearchByName(string searchTerm)
        {
            return _repository.GetAll()
                .Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Name);
        }

        public IEnumerable<Contact> FilterByTag(string tag)
        {
            return _repository.GetAll()
                .Where(c => c.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                .OrderBy(c => c.Name);
        }
    }
}
