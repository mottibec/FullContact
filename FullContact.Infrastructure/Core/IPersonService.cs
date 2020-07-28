using System.Threading.Tasks;

namespace FullContact.Infrastructure
{
    public interface IPersonService
    {
        string Name { get; }

        Task<IPersonResponse> GetPerson(string identifier);

        string GetIdentifier();
    }
}
