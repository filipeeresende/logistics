using Logistics.Domain.Interfaces.Repositories;
using Logistics.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Repositories
{
    public  class BaseRepository<TEntity> : IBaseRepository<TEntity> 
    {
        protected readonly LogisticsContext _context;
        public BaseRepository(LogisticsContext context)
        {
            _context = context;
        }
        public async Task InsertAsync(TEntity insert)
        {
            await _context.AddAsync(insert);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(TEntity delete)
        {
            _context.Remove(delete);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity update)
        {
            _context.Update(update);
            await _context.SaveChangesAsync();
        }
    }
}
