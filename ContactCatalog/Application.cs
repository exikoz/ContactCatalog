using ContactCatalog.Services;
using ContactCatalog.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog
{
    public class Application
    {
        private readonly ContactService _service;
        private readonly ConsoleMenu _menu;

        public Application()
        {
            var repository = new ContactRepository();
            _service = new ContactService(repository);
            _menu = new ConsoleMenu(_service);
        }
        public void Run()
        {
            _menu.Run();

        }
    }
}
