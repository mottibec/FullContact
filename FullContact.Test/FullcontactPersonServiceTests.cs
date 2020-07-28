using FullContact.Infrastructure;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace FullContact.Test
{
    [TestFixture]
    public class FullcontactPersonServiceTests
    {
        private IPersonService _service;
        [SetUp]
        public void Setup()
        {
            _service = new FullcontactPersonService("full contact", "https://api.fullcontact.com", "apikey");
        }

        [Test]
        public void create_service_success()
        {
            var service = new FullcontactPersonService("full contact", "https://api.fullcontact.com", "apikey");
            Assert.IsNotNull(service);
        }

        [Test]
        public void create_service_null()
        {
            Assert.Throws<ArgumentNullException>(() => new FullcontactPersonService(null, "http://test.com", null));

            Assert.Throws<ArgumentNullException>(() => new FullcontactPersonService("name", null, null));

            var nullApiKey = new FullcontactPersonService("name", "http://test.com", null);
            Assert.IsNotNull(nullApiKey);
        }


        [Test]
        public async Task get_person()
        {
            var person = await _service.GetPerson("mybechhofer@gmail.com");
            Assert.IsNotNull(person);
        }


        [Test]
        public void get_non_exsit_person()
        {
            Assert.ThrowsAsync<HttpRequestException>(() => _service.GetPerson("dosenot@exsit.com"));
        }
    }
}