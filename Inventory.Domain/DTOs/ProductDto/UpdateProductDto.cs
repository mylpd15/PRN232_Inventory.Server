using System.ComponentModel.DataAnnotations;
namespace Inventory.Domain
{
    public class UpdateProductDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
