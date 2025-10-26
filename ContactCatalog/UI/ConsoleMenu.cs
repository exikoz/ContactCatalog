using ContactCatalog.Services;
using ContactCatalog.Models;
using ContactCatalog.Exceptions;
using ContactCatalog.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.UI
{
    public class ConsoleMenu
    {
        private readonly ContactService _service;

        public ConsoleMenu(ContactService service)
        {
            _service = service;
        }

        public void Run()
        {
            while (true)
            {

                Console.Clear();
                DisplayMenu();

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddContact();
                        break;
                    case "2":
                        ListContacts();
                        break;
                    case "3":
                        SearchContacts();
                        break;
                    case "4":
                        FilterByTag();
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("=== Contact Catalog ===");
            Console.WriteLine("1) Add");
            Console.WriteLine("2) List");
            Console.WriteLine("3) Search (Name contains)");
            Console.WriteLine("4) Filter by Tag");
            Console.WriteLine("0) Exit");
            Console.Write("> ");
        }

        private void AddContact()
        {
            ConsoleHelper.WriteHeader("=== Add Contact ===\n");

            int id;
            while (true)
            {
                Console.Write("Id: ");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    break;
                }
                ConsoleHelper.WriteError("Invalid ID. Please enter a number.");
            }

            Console.Write("Name: ");
            var name = Console.ReadLine() ?? "";

            string email;
            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(email))
                {
                    ConsoleHelper.WriteError("Email cannot be empty.");
                    continue;
                }

                if (!EmailValidator.IsValidEmail(email))
                {
                    ConsoleHelper.WriteError("Invalid email format. Please try again.");
                    continue;
                }

                break;
            }

            Console.Write("Tags (comma-separated): ");
            var tagsInput = Console.ReadLine() ?? "";
            var tags = tagsInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(t => t.Trim())
                                .ToList();

            var contact = new Contact
            {
                Id = id,
                Name = name,
                Email = email,
                Tags = tags
            };

            try
            {
                _service.AddContact(contact);
                ConsoleHelper.WriteSuccess("\n[Added!] Contact added successfully.");
            }
            catch (DuplicationEmailException ex)
            {
                ConsoleHelper.WriteError($"\n[Error] {ex.Message}");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"\n[Error] {ex.Message}");
            }

            PressAnyKeyToContinue();
        }

        private void ListContacts()
        {
            ConsoleHelper.WriteHeader("=== All Contacts ===\n");

            var contacts = _service.GetAllContacts().ToList();

            if (contacts.Count == 0)
            {
                ConsoleHelper.WriteError("No contacts in catalog.");
            }
            else
            {
                Console.WriteLine($"{contacts.Count} contact(s):");
                foreach (var contact in contacts)
                {
                    ConsoleHelper.WriteInfo($"- {contact}");
                }
            }

            PressAnyKeyToContinue();
        }

        private void SearchContacts()
        {
            ConsoleHelper.WriteHeader("=== Search by Name ===\n");

            Console.Write("Search term for name: ");
            var searchTerm = Console.ReadLine() ?? "";

            var results = _service.SearchByName(searchTerm).ToList();

            if (results.Count == 0)
            {
                ConsoleHelper.WriteError("\nNo matches found.");
            }
            else
            {
                Console.WriteLine($"\n{results.Count} match(es):");
                foreach (var contact in results)
                {
                    ConsoleHelper.WriteInfo($"- {contact}");
                }
            }

            PressAnyKeyToContinue();
        }

        private void FilterByTag()
        {
            ConsoleHelper.WriteHeader("=== Filter by Tag ===\n");

            Console.Write("Tag: ");
            var tag = Console.ReadLine() ?? "";

            var results = _service.FilterByTag(tag).ToList();

            if (results.Count == 0)
            {
                ConsoleHelper.WriteError("\nNo contacts with that tag.");
            }
            else
            {
                Console.WriteLine($"\n{results.Count} contact(s) with tag '{tag}':");
                foreach (var contact in results)
                {
                    ConsoleHelper.WriteInfo($"- {contact}");
                }
            }

            PressAnyKeyToContinue();
        }

        private void PressAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

    }
}
