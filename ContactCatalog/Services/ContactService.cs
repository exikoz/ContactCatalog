using ContactCatalog.Models;
using Microsoft.Extensions.Logging;

namespace ContactCatalog.Services;

public class ContactService
{
    private readonly IContactRepository _repository;
    private readonly ILogger<ContactService> _logger;

    public ContactService(IContactRepository repository, ILogger<ContactService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public void AddContact(Contact contact)
    {
        _logger.LogInformation("ContactService: Adding contact {Name}", contact.Name);
        _repository.Add(contact);
    }

    public IEnumerable<Contact> GetAllContacts()
    {
        _logger.LogInformation("ContactService: Getting all contacts");
        return _repository.GetAll();
    }

    public IEnumerable<Contact> SearchByName(string searchTerm)
    {
        _logger.LogInformation("ContactService: Searching contacts by name containing '{SearchTerm}'", searchTerm);

        var results = _repository.GetAll()
            .Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderBy(c => c.Name);

        var resultList = results.ToList();
        _logger.LogInformation("Search found {Count} match(es)", resultList.Count);

        return resultList;
    }

    public IEnumerable<Contact> FilterByTag(string tag)
    {
        _logger.LogInformation("ContactService: Filtering contacts by tag '{Tag}'", tag);

        var results = _repository.GetAll()
            .Where(c => c.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
            .OrderBy(c => c.Name);

        var resultList = results.ToList();
        _logger.LogInformation("Filter found {Count} contact(s) with tag '{Tag}'", resultList.Count, tag);

        return resultList;
    }
}