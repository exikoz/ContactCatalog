namespace ContactCatalog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Contact Catalog - Testing Contact Model ===\n");

            var contact1 = new Contact
            {
                Id = 1,
                Name = "John Doeson",
                Email = "j.d@test.com",
                Tags = new List<string> { "firend", "gym" }

            };

            var contact2 = new Contact
            {
                Id = 2,
                Name = "Bo Bengtsson",
                Email = "bo@example.com",
                Tags = new List<string> { "work", "colleague" }
            };

            var contact3 = new Contact
            {
                Id = 3,
                Name = "Cecilia Carlsson",
                Email = "cecilia@example.com",
                Tags = new List<string>()
            };

            Console.WriteLine("Test Contacts");
            Console.WriteLine(contact1);
            Console.WriteLine(contact2);
            Console.WriteLine(contact3);

            Console.WriteLine("\n Contact working");

        }
    }
}
