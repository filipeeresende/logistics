using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task InsertAsync(TEntity insert);
        Task DeleteAsync(TEntity delete);
        Task UpdateAsync(TEntity update);
    }
}
