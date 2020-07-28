using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FullContact.Infrastructure
{
    public class FullContactPersonResponse : IPersonResponse
    {
        [JsonProperty("ageRange")]
        public string Age { get; set; }

        [JsonProperty("fullName")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        private string _city;
        public string City { get => Location.Split(' ').FirstOrDefault(); set => _city = value; }

        private string _country;
        public string Country { get => Location.Split(' ').LastOrDefault(); set => _city = value; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}
