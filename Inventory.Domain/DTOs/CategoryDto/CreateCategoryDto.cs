using System.ComponentModel.DataAnnotations;
namespace Inventory.Domain
{
    public class CreateCategoryDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}
