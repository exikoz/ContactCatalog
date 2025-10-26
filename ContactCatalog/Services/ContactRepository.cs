using ContactCatalog.Models;
using ContactCatalog.Exceptions;
using ContactCatalog.Validators;
using Microsoft.Extensions.Logging;

namespace ContactCatalog.Services;

public class ContactRepository : IContactRepository
{
    private readonly Dictionary<int, Contact> _contactsById;
    private readonly HashSet<string> _emails;
    private readonly ILogger<ContactRepository> _logger;

    public ContactRepository(ILogger<ContactRepository> logger)
    {
        _contactsById = new Dictionary<int, Contact>();
        _emails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        _logger = logger;
    }

    public void Add(Contact contact)
    {
        _logger.LogInformation("Attempting to add contact with ID {Id} and email {Email}", contact.Id, contact.Email);

        if (_contactsById.ContainsKey(contact.Id))
        {
            _logger.LogWarning("Duplicate ID detected: {Id}", contact.Id);
            throw new DuplicateIdException(contact.Id);
        }

        if (!EmailValidator.IsValidEmail(contact.Email))
        {
            _logger.LogWarning("Invalid email format: {Email}", contact.Email);
            throw new InvalidEmailException(contact.Email);
        }

        if (!_emails.Add(contact.Email))
        {
            _logger.LogWarning("Duplicate email detected: {Email}", contact.Email);
            throw new DuplicationEmailException(contact.Email);
        }


        _contactsById.Add(contact.Id, contact);
        _logger.LogInformation("Successfully added contact: {Name} ({Id})", contact.Name, contact.Id);
    }

    public IEnumerable<Contact> GetAll()
    {
        _logger.LogInformation("Retrieving all contacts. Total count: {Count}", _contactsById.Count);
        return _contactsById.Values;
    }

    public Contact? GetById(int id)
    {
        _logger.LogInformation("Searching for contact with ID {Id}", id);
        _contactsById.TryGetValue(id, out var contact);

        if (contact == null)
        {
            _logger.LogWarning("Contact with ID {Id} not found", id);
        }

        return contact;
    }
}