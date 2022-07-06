using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels.Request
{
    public class CreateProductRequest
    {
        [Required]
        public long CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public double Price { get; set; }

        public double? DiscountPercentage { get; set; }
        public double? PriceForTwoPieces { get; set; }
    }
}
