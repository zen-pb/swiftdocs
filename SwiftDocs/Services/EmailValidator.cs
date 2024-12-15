using System;
using System.Text.RegularExpressions;

namespace SwiftDocs.Services
{
    public class EmailValidator
    {
        public bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, pattern);
        }
        
        public bool TryValidate(string email, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                errorMessage = "Email address cannot be empty.";
                return false;
            }

            if (!IsValid(email))
            {
                errorMessage = "The email address format is invalid.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}