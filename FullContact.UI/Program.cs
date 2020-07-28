using FullContact.Domain;
using FullContact.Domain.Exceptions;
using FullContact.Domain.Model;
using FullContact.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullContact.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var user = new User(10);
            IPersonService selectedService = SelectPersonService();
            Console.WriteLine($"{selectedService.Name} requires {selectedService.GetIdentifier()}");
            var identifier = Console.ReadLine();
            try
            {
                Person person = await user.GetPerson(selectedService, identifier);
                Console.WriteLine($"person {person.Name}");
            }
            catch (PartialInformationException ex)
            {
                Console.WriteLine($"person with {selectedService.GetIdentifier()} {identifier} returned partial results");
            }
            catch (PersonNotFoundException ex)
            {

                Console.WriteLine($"person with {selectedService.GetIdentifier()} {identifier} was not found on {selectedService.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error while getting person, {ex.Message}");
            }
        }

        private static IPersonService SelectPersonService()
        {
            Console.WriteLine($"select a service for data retrieval");
            var personServices = GetServices();
            for (int i = 0; i < personServices.Count(); i++)
            {
                Console.WriteLine($"[{i}] {personServices.ElementAt(i).Name}");
            }

            var selectedInput = Console.ReadLine();
            var parseResult = int.TryParse(selectedInput, out var selectedServiceIndex);
            if (!parseResult
                || selectedServiceIndex < 0
                || selectedServiceIndex > personServices.Count())
            {
                throw new ArgumentOutOfRangeException(nameof(selectedServiceIndex));
            }

            return personServices.ElementAt(selectedServiceIndex);
        }

        private static IEnumerable<IPersonService> GetServices()
        {
            //todo store api key in secure storage
            return new IPersonService[]
            {
                new FullcontactPersonService("full contact", "https://api.fullcontact.com", "API_KEY")
            };
        }
    }
}
