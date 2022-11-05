using Logistics.Domain.Dto.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderResponse> GetOrderById(int id);
        Task<IList<OrdersResponse>> GetOrders();
        Task<string>InsertOrder(InsertOrderRequest order);
        Task<string>DeleteOrder(int id);
    }
}
