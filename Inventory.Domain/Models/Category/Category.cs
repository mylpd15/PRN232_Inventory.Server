using System.ComponentModel.DataAnnotations;
namespace Inventory.Domain
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
