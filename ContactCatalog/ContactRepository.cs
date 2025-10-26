using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog
{
    public class ContactRepository : IContactRepository
    {
        private readonly Dictionary<int, Contact> _contactsById;
        private readonly HashSet<string> _emails;

        public ContactRepository()
        {
            _contactsById = new Dictionary<int, Contact>();
            _emails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        public void Add(Contact contact)
        {
            if (!EmailValidator.IsValidEmail(contact.Email)) {
                throw new InvalidEmailException(contact.Email);
            }

            if (!_emails.Add(contact.Email)) {
                throw new DuplicateWaitObjectException(contact.Email);
            }

            _contactsById.Add(contact.Id, contact);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contactsById.Values;
        }

        public Contact? GetById(int id)
        {
            _contactsById.TryGetValue(id, out var contact);
            return contact;
        }
    }
}
