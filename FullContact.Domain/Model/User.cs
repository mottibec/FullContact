using FullContact.Domain.Exceptions;
using FullContact.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullContact.Domain.Model
{
    public class User
    {
        private readonly int _requestsRateLimit;
        private readonly ConcurrentDictionary<string, RequestHistory> _requestHistory;
        private int _requestCount;

        public User(int userRateLimit)
        {
            _requestsRateLimit = userRateLimit;
            _requestHistory = new ConcurrentDictionary<string, RequestHistory>();
        }

        private TResult RateLimtAction<TResult>(Func<TResult> action)
        {
            if (_requestCount >= _requestsRateLimit)
            {
                throw new RateLimitExceededException(_requestsRateLimit);
            }
            try
            {
                return action();
            }
            finally
            {
                _requestCount++;
            }
        }

        public async Task<Person> GetPerson(IPersonService personService, string identifier)
        {
            try
            {
                return await RateLimtAction(async () =>
                  {
                      var response = await personService.GetPerson(identifier);
                      var person = new Person(response.Name, response.Age, response.City, response.Country);
                      _requestHistory.AddOrUpdate(identifier, identifier => new RequestHistory(person), (key, requestHistory) =>
                      {
                          requestHistory.UpateCount();
                          return requestHistory;
                      });
                      return person;
                  });
            }
            catch (ArgumentNullException ex)
            {
                throw new PartialInformationException(identifier);
            }
            catch (HttpRequestException ex)
            {
                throw new PersonNotFoundException(identifier);
            }
        }

        public int GetRequestCount(string identifier)
        {
            var requestHistory = _requestHistory[identifier];
            return requestHistory.Count;
        }
    }
}
