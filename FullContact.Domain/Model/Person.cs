using System;

namespace FullContact.Domain.Model
{
    public class Person
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Age { get; private set; }
        public string Address => $"{_city}-{_country}";

        private string _city;
        private string _country;

        public Person(string name, string age, string city, string country)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Age = age ?? throw new ArgumentNullException(nameof(age));
            _city = city ?? throw new ArgumentNullException(nameof(city));
            _country = country ?? throw new ArgumentNullException(nameof(country));
        }
    }
}
