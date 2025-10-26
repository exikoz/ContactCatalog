using ContactCatalog.Models;
using ContactCatalog.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace ContactCatalog.Tests;

public class ContactServiceTests
{
    private readonly Mock<ILogger<ContactService>> _mockLogger;

    public ContactServiceTests()
    {
        _mockLogger = new Mock<ILogger<ContactService>>();
    }

    [Fact]
    public void FilterByTag_ReturnsOnlyMatchingContacts()
    {
        // Arrange
        var mockRepo = new Mock<IContactRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new[]
        {
            new Contact { Id = 1, Name = "Anna", Email = "anna@example.com", Tags = new List<string> { "friend" } },
            new Contact { Id = 2, Name = "Bo", Email = "bo@example.com", Tags = new List<string> { "work" } },
            new Contact { Id = 3, Name = "Cecilia", Email = "cecilia@example.com", Tags = new List<string> { "friend", "gym" } }
        });

        var service = new ContactService(mockRepo.Object, _mockLogger.Object);

        // Act
        var result = service.FilterByTag("friend").ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Name == "Anna");
        Assert.Contains(result, c => c.Name == "Cecilia");
    }

    [Fact]
    public void SearchByName_ReturnsCaseInsensitiveMatches()
    {
        // Arrange
        var mockRepo = new Mock<IContactRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new[]
        {
            new Contact { Id = 1, Name = "Anna Andersson", Email = "anna@example.com", Tags = new List<string>() },
            new Contact { Id = 2, Name = "Bo Bengtsson", Email = "bo@example.com", Tags = new List<string>() },
            new Contact { Id = 3, Name = "Anna-Karin", Email = "ak@example.com", Tags = new List<string>() }
        });

        var service = new ContactService(mockRepo.Object, _mockLogger.Object);

        // Act
        var result = service.SearchByName("anna").ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, c => Assert.Contains("anna", c.Name, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void FilterByTag_ReturnsOrderedResults()
    {
        // Arrange
        var mockRepo = new Mock<IContactRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new[]
        {
            new Contact { Id = 3, Name = "Cecilia", Email = "c@example.com", Tags = new List<string> { "work" } },
            new Contact { Id = 1, Name = "Anna", Email = "a@example.com", Tags = new List<string> { "work" } },
            new Contact { Id = 2, Name = "Bo", Email = "b@example.com", Tags = new List<string> { "work" } }
        });

        var service = new ContactService(mockRepo.Object, _mockLogger.Object);

        // Act
        var result = service.FilterByTag("work").ToList();

        // Assert
        Assert.Equal("Anna", result[0].Name);
        Assert.Equal("Bo", result[1].Name);
        Assert.Equal("Cecilia", result[2].Name);
    }
}