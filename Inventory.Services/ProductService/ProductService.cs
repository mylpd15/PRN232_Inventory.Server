using AutoMapper;
using AutoMapper.QueryableExtensions;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;
namespace Inventory.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public ProductService(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<PageDto<ProductDto>> GetPagedAsync(int page, int take, string? search)
        {
            var query = _ctx.Products
                            .Include(p => p.Category)
                            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search));

            var total = await query.CountAsync();

            var data = await query
                .OrderBy(p => p.Name)
                .Skip((page - 1) * take)
                .Take(take)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var meta = new PageMetaDto(new PageQueryDto { Page = page, Take = take }, total);
            return new PageDto<ProductDto>(data, meta);
        }
        public IQueryable<ProductDto> GetQueryable()
        {
            return _ctx.Products
                       .Include(p => p.Category) 
                       .OrderBy(p => p.Name)     
                       .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var entity = await _ctx.Products
                                   .Include(p => p.Category)
                                   .FirstOrDefaultAsync(p => p.Id == id);
            return entity == null
                ? null
                : _mapper.Map<ProductDto>(entity);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            _ctx.Products.Add(entity);
            await _ctx.SaveChangesAsync();
            // load Category navigation for mapping
            await _ctx.Entry(entity).Reference(e => e.Category).LoadAsync();
            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<ProductDto?> UpdateAsync(UpdateProductDto dto)
        {
            var entity = await _ctx.Products.FindAsync(dto.Id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _ctx.SaveChangesAsync();

            await _ctx.Entry(entity).Reference(e => e.Category).LoadAsync();
            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _ctx.Products.FindAsync(id);
            if (entity == null) return false;

            _ctx.Products.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
