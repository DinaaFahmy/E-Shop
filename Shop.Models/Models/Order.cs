using Shop.Models.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models.Models
{
    public class Order : ISqlEntity
    {
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        [ForeignKey(nameof(Product))]
        public long? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
