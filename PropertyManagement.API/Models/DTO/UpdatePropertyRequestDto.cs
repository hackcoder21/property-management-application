using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.API.Models.DTO
{
    public class UpdatePropertyRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must have at least 5 characters.")]
        [MaxLength(150, ErrorMessage = "Title cannot exceed 150 characters.")]
        public string Title { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string State { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        public string Locality { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Pincode must be exactly 6 digits.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must contain only digits.")]
        public string Pincode { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Number of rooms must be between 1 and 50.")]
        public int NoOfRooms { get; set; }

        [Required]
        public float CarpetAreaSqft { get; set; }

        [Required]
        [Range(1800, 2100, ErrorMessage = "Built year must be between 1800 and 2100.")]
        public int BuiltYear { get; set; }

        [Required]
        public bool Balcony { get; set; }

        [Required]
        public bool Parking { get; set; }

        public string? PropertyImageUrl { get; set; }

        public string? HallImageUrl { get; set; }

        public string? KitchenImageUrl { get; set; }

        public string? BathroomImageUrl { get; set; }

        public string? BedroomImageUrl { get; set; }

        public string? ParkingImageUrl { get; set; }
    }
}