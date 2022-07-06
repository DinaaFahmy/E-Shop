using Shop.Models.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models.Models
{
    public class Product : ISqlEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? DiscountPercentage { get; set; }
        public double? PriceForTwoPieces { get; set; }
        [ForeignKey(nameof(Category))]
        public long? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
