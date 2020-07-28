using System;

namespace FullContact.Domain.Exceptions
{
    public class PartialInformationException : Exception
    {
        public PartialInformationException(string identifier)
            : base($"only partial info found for {identifier}")
        {

        }
    }
}
