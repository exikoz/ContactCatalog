using ContactCatalog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog
{
    public class Application
    {
        private ContactService _service;

        public Application()
        {
            var repository = new ContactRepository();
            _service = new ContactService(repository);
        }
        public void Run()
        {
            Console.WriteLine("=== Contact Catalog ===\n");

        }
    }
}
