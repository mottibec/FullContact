using System;

namespace FullContact.Domain.Exceptions
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException(string identifier)
            : base($"person with {identifier} was not found")
        {

        }
    }
}
