using System;
using System.Collections.Generic;
using System.Text;

namespace FullContact.Domain.Model
{
    public class RequestHistory
    {
        private Person _person;
        private int _requestCount;

        public int Count => _requestCount;

        public RequestHistory(Person person)
        {
            _person = person;
            _requestCount = 1;
        }

        public void UpateCount() 
        {
            _requestCount++;
        }
    }
}
