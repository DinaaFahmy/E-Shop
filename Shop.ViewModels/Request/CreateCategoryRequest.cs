using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels.Request
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
