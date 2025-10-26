using ContactCatalog.Models;
using ContactCatalog.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Tests
{
    public class ContactServiceTests
    {
        [Fact]
        public void FilterByTag_ReturnsOnlyMatchingContacts()
        {
            // Arrange
            var mockRepo = new Mock<IContactRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(new[]
            {
                new Contact { Id = 1, Name = "John", Email = "j@test.com", Tags = new List<string> {"friend"} },
                new Contact { Id = 2, Name = "Bo", Email = "bo@test.com", Tags = new List<string> { "work" } },
                new Contact { Id = 3, Name = "Cecilia", Email = "cecilia@test.com", Tags = new List<string> { "friend", "gym" } }
            });

            var service = new ContactService(mockRepo.Object);

            // Act
            var result = service.FilterByTag("friend").ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "John");
            Assert.Contains(result, c => c.Name == "Cecilia");
            Assert.DoesNotContain(result, c => c.Name == "Bo");
        }
    }
}
