namespace PropertyManagement.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<Property> Properties { get; set; } = new List<Property>();
    }
}
