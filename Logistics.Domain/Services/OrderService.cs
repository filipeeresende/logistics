using Logistics.Domain.Constants;
using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.Domain.Settings.ErrorHandler.ErrorStatusCodes;
using Logistics.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPedidoRepository _pedidoRepository;
        public OrderService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<OrderResponse> GetOrderById(int id)
        {
            OrderResponse order = await _pedidoRepository.GetOrderById(id);

            if (order == null)
                throw new NotFoundException(ReturnMessageOrder.MessageOrderNotFound);

            return order;
        }
        public async Task<IList<OrdersResponse>> GetOrders()
        {
            IList<OrdersResponse> orders = await _pedidoRepository.GetOrders();

            if (!orders.Any())
                throw new NotFoundException(ReturnMessageOrder.MessageOrdersNotFound);

            return orders;
        }
        public async Task<string>InsertOrder(InsertOrderRequest order)
        {
            await _pedidoRepository.InsertAsync(OrderUtils.AddOrderMapper(order));
            return ReturnMessageOrder.MessageOrdersRegistered;
        }
        public async Task<string>DeleteOrder(int id)
        {
            Pedido order = await _pedidoRepository.GetOrderByIdObject(id);

            if (order == null)
                throw new NotFoundException(ReturnMessageOrder.MessageOrderNotFound);

            await _pedidoRepository.DeleteAsync(order);
            return ReturnMessageOrder.MessageOrdersDelete;
        }
        public async Task<string> UpdateOrder(UpdateOrderRequest updateOrderRequest, int id)
        {
            Pedido order = await _pedidoRepository.GetOrderByIdObject(id);

            if (order == null)
                throw new NotFoundException(ReturnMessageOrder.MessageOrderNotFound);

            order.IndCancelado = updateOrderRequest.IndCancelado;
            order.IndConcluido = updateOrderRequest.IndConcluido;

            await _pedidoRepository.UpdateAsync(order);
            return ReturnMessageOrder.MessageOrderUpdate;
        }

    }
}
