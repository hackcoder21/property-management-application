namespace PropertyManagement.API.Models.DTO
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<UserPropertySummaryDto> Properties { get; set; }
    }

    public class UserPropertySummaryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
    }
}