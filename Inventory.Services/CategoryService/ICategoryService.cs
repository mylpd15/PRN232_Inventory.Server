using Inventory.Domain;
namespace Inventory.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(Guid id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<CategoryDto?> UpdateAsync(UpdateCategoryDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
