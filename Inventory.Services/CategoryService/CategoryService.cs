using AutoMapper;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;
namespace Inventory.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public CategoryService(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var entities = await _ctx.Categories
                .OrderBy(x => x.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(entities);
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            var entity = await _ctx.Categories.FindAsync(id);
            return entity is null ? null : _mapper.Map<CategoryDto>(entity);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            _ctx.Categories.Add(entity);
            await _ctx.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<CategoryDto?> UpdateAsync(UpdateCategoryDto dto)
        {
            var entity = await _ctx.Categories.FindAsync(dto.Id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _ctx.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _ctx.Categories.FindAsync(id);
            if (entity == null) return false;

            _ctx.Categories.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
