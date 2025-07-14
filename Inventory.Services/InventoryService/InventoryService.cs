using AutoMapper;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.InventoryService
{
    public class InventoryService : IInventoryService
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public InventoryService(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<InventoryEntryDto> CreateEntryAsync(CreateInventoryEntryDto dto)
        {
            var entry = _mapper.Map<InventoryEntry>(dto);
            _ctx.InventoryEntries.Add(entry);
            await _ctx.SaveChangesAsync();

            await _ctx.Entry(entry).Reference(x => x.Product).LoadAsync();
            return _mapper.Map<InventoryEntryDto>(entry);
        }

        public async Task<PageDto<InventoryEntryDto>> GetHistoryAsync(Guid productId, int page, int take)
        {
            var pageQuery = new PageQueryDto { Page = page, Take = take };

            var query = _ctx.InventoryEntries
                            .Include(x => x.Product)
                            .Where(x => x.ProductId == productId);

            var total = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.Timestamp)
                .Skip(pageQuery.Skip())
                .Take(pageQuery.Take)
                .ToListAsync();

            return new PageDto<InventoryEntryDto>(
                _mapper.Map<IEnumerable<InventoryEntryDto>>(data),
                new PageMetaDto(pageQuery, total)
            );
        }
    }
}
