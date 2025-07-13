using Inventory.Domain;
namespace Inventory.Services
{
    public interface IProductService
    {
        Task<PageDto<ProductDto>> GetPagedAsync(int page, int take, string? search);
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<ProductDto?> UpdateAsync(UpdateProductDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
