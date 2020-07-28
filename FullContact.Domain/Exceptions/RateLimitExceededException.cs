using System;

namespace FullContact.Domain.Exceptions
{
    public class RateLimitExceededException : Exception
    {
        public RateLimitExceededException(int rateLimit)
            :base($"your {rateLimit} rate limit was exceeded")
        {

        }
    }
}
