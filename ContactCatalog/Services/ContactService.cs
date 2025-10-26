using ContactCatalog.Models;
using Microsoft.Extensions.Logging;
using System.Text;

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

    public void ExportToCsv(string filePath)
    {
        _logger.LogInformation("ContactService: Exporting contacts to CSV file '{FilePath}'", filePath);

        var contacts = _repository.GetAll().ToList();

        if (contacts.Count == 0)
        {
            _logger.LogWarning("No contacts to export");
            throw new InvalidOperationException("No contacts to export");
        }

        var csv = ToCsv(contacts);
        File.WriteAllText(filePath, csv);

        _logger.LogInformation("Successfully exported {Count} contact(s) to '{FilePath}'", contacts.Count, filePath);
    }

    private string ToCsv(IEnumerable<Contact> contacts)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,Name,Email,Tags");

        foreach (var contact in contacts)
        {
            var tags = string.Join('|', contact.Tags);
            sb.AppendLine($"{contact.Id},{contact.Name},{contact.Email},{tags}");
        }

        return sb.ToString();
    }
}