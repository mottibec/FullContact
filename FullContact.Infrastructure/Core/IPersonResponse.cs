namespace FullContact.Infrastructure
{
    public interface IPersonResponse
    {
        string Name { get; set; }
        string Age { get; set; }
        string City { get; set; }
        string Country { get; set; }
        string Gender { get; set; }
    }
}
