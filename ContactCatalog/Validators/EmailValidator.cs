using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Validators
{
    
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) 
                return false;

            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch { 
                return false; 
            }
        }
    }
}
