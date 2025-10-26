using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new();

        public override string ToString()
        {
            var tagsString = Tags.Count > 0 ? string.Join(", ", Tags) : "no tags";
            return $"({Id}) {Name} <{Email}> [{tagsString}]";
        }
    }
}
