using AutoMapper;
using Inventory.Domain;
namespace Inventory.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // === Product ===
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryName,
                           o => o.MapFrom(s => s.Category.Name));
            // Create Product
            CreateMap<CreateProductDto, Product>();
            // Update Product
            CreateMap<UpdateProductDto, Product>();


            // === Category ===
            CreateMap<Category, CategoryDto>().ReverseMap();
            // Create Category
            CreateMap<CreateCategoryDto, Category>();
            // Update Category
            CreateMap<UpdateCategoryDto, Category>();


            // === InventoryEntry ===
            CreateMap<InventoryEntry, InventoryEntryDto>()
                .ForMember(d => d.ProductName,
                           o => o.MapFrom(s => s.Product.Name)).ReverseMap();
            // Create InventoryEntry
            CreateMap<CreateInventoryEntryDto, InventoryEntry>()
                .ForMember(d => d.Timestamp,
                           o => o.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
