namespace PropertyManagement.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Property Property { get; set; }
    }
}
