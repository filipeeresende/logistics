using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        Task InsertAsync<TEntry>(TEntry insert);
        Task DeleteAsync<TEntry>(TEntry delete);
        Task UpdateAsync<TEntry>(TEntry update);
    }
}
