using Logistics.Domain.Interfaces.Repositories;
using Logistics.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Repositories
{
    public  class BaseRepository : IBaseRepository
    {
        private readonly LogisticsContext _context;
        public BaseRepository(LogisticsContext context)
        {
            _context = context;
        }
        public async Task InsertAsync<TEntry>(TEntry insert)
        {
            await _context.AddAsync(insert);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync<TEntry>(TEntry delete)
        {
            _context.Remove(delete);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync<TEntry>(TEntry update)
        {
            _context.Update(update);
            await _context.SaveChangesAsync();
        }
    }
}
