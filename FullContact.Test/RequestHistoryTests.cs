using FullContact.Domain.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullContact.Test
{
    [TestFixture]
    public class RequestHistoryTests
    {
        [Test]
        public void test_request_history_count() 
        {
            var requestHistory = new RequestHistory(new Person("name", "age", "tel aviv", "israel"));
            Assert.That(requestHistory.Count, Is.EqualTo(1));
            requestHistory.UpateCount();
            Assert.That(requestHistory.Count, Is.EqualTo(2));
        }
    }
}
