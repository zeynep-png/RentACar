using Microsoft.AspNetCore.DataProtection;
using RentACar.Business.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.DataProtection
{
    
    public class DataProtection : IDataProtection
    {
        // Field to hold the IDataProtector instance
        private readonly IDataProtector _protector;

        // Constructor initializes the _protector field with a data protector using a unique purpose string
        public DataProtection(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("RentACar-security-v1");
        }

        // Method to encrypt the input text and return the protected string
        public string Protect(string text)
        {
            return _protector.Protect(text);
        }

        // Method to decrypt the protected input text and return the original string
        public string UnProtect(string protectedText)
        {
            return _protector.Unprotect(protectedText);
        }
    }
}
