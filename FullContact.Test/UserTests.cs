using FullContact.Domain.Exceptions;
using FullContact.Domain.Model;
using FullContact.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace FullContact.Test
{
    [TestFixture]
    public class UserTests
    {
        private IPersonService _validService;
        private IPersonService _notFoundService;
        private IPersonService _partialService;

        [SetUp]
        public void SetUp()
        {
            var validPersonServiceMock = new Mock<IPersonService>();
            var personResponseMock = new Mock<IPersonResponse>();
            personResponseMock.Setup(x => x.Name).Returns("Name");
            personResponseMock.Setup(x => x.Age).Returns("Age");
            personResponseMock.Setup(x => x.City).Returns("City");
            personResponseMock.Setup(x => x.Country).Returns("Country");
            personResponseMock.Setup(x => x.Gender).Returns("Gender");
            validPersonServiceMock.Setup(ps => ps.GetPerson("person")).Returns(Task.FromResult(personResponseMock.Object));
            _validService = validPersonServiceMock.Object;

            var notFoundPersonServiceMock = new Mock<IPersonService>();
            notFoundPersonServiceMock.Setup(ps => ps.GetPerson("person")).Throws(new Exception());
            _notFoundService = notFoundPersonServiceMock.Object;

            var partialPersonServiceMock = new Mock<IPersonService>();
            var partialPersonResponseMock = new Mock<IPersonResponse>();
            personResponseMock.Setup(x => x.Name).Returns<IPersonResponse, string>(null);
            partialPersonServiceMock.Setup(ps => ps.GetPerson("person")).Returns(Task.FromResult(personResponseMock.Object));
            _partialService = partialPersonServiceMock.Object;
        }

        [Test]
        public async Task user_get_person()
        {
            var user = new User(10);
            var person = await user.GetPerson(_validService, "person");
            Assert.IsNotNull(person);
            Assert.That(person.Name, Is.EqualTo("Name"));
        }

        [Test]
        public void user_person_not_found()
        {
            var user = new User(10);
            Assert.ThrowsAsync<PersonNotFoundException>(() => user.GetPerson(_notFoundService, "person"));
        }

        [Test]
        public void user_partial_person_found()
        {
            var user = new User(10);
            Assert.ThrowsAsync<PartialInformationException>(() => user.GetPerson(_partialService, "person"));
        }

        [Test]
        public async Task user_rate_limit_exceeded()
        {
            var ratelimit = 5;
            var user = new User(ratelimit);

            for (int i = 1; i <= ratelimit; i++)
            {
                var person = await user.GetPerson(_validService, "person");
            }
            Assert.ThrowsAsync<RateLimitExceededException>(() => user.GetPerson(_validService, "person"));
        }

        [Test]
        public async Task user_history_updates()
        {
            var ratelimit = 5;
            var identifier = "person";
            var user = new User(ratelimit);

            for (int i = 1; i <= ratelimit; i++)
            {
                var person = await user.GetPerson(_validService, identifier);
            }
            var requestCount = user.GetRequestCount(identifier);

            Assert.That(requestCount, Is.EqualTo(ratelimit));
        }
    }
}
