using PropertyManagement.API.Models.Domain;

namespace PropertyManagement.API.Models.DTO
{
    public class PropertyDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Locality { get; set; }
        public string Pincode { get; set; }
        public int NoOfRooms { get; set; }
        public float CarpetAreaSqft { get; set; }
        public int BuiltYear { get; set; }
        public bool Balcony { get; set; }
        public bool Parking { get; set; }
        public string? PropertyImageUrl { get; set; }
        public string? HallImageUrl { get; set; }
        public string? KitchenImageUrl { get; set; }
        public string? BathroomImageUrl { get; set; }
        public string? BedroomImageUrl { get; set; }
        public string? ParkingImageUrl { get; set; }
    }
}
