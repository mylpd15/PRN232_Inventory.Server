using System.ComponentModel.DataAnnotations;
namespace Inventory.Domain
{
    public class UpdateCategoryDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}
