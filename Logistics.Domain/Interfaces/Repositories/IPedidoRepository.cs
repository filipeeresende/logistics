using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        Task<OrderResponse> GetOrderById(int id);
        Task<IList<OrdersResponse>> GetOrders();
        Task<Pedido> GetOrderByIdObject(int id);
        Task<bool> CheckIfOrderExists(int id);
    }
}
