using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly LogisticsContext _context;
        public PedidoRepository(LogisticsContext context)
        {
            _context = context;
        }
        public async Task<OrderResponse> GetOrderById(int id)
        {
            return await _context.Pedidos
                .Where(x => x.Id == id)
                .Select(x => new OrderResponse
                {
                    NumeroPedido = x.NumeroPedido,
                    HoraPedido = x.HoraPedido,
                    IndCancelado = x.IndCancelado,
                    IndConcluido = x.IndConcluido
                }).FirstOrDefaultAsync();
        }
        public async Task<IList<OrdersResponse>> GetOrders()
        {
            return await _context.Pedidos
              .Select(x => new OrdersResponse
              {
                  Id = x.Id,
                  NumeroPedido = x.NumeroPedido,
                  HoraPedido = x.HoraPedido,
                  IndCancelado = x.IndCancelado,
                  IndConcluido = x.IndConcluido
              }).ToListAsync();
        }
        public async Task<Pedido> GetOrderByIdObject(int id)
        {
            return await _context.Pedidos
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<OrderResponse> CheckIfOrderExists(int id)
        {
            return await _context.Pedidos
                .Where(x => x.Id == id)
                .Select(x => new OrderResponse
                {
                    NumeroPedido = x.NumeroPedido,

                }).FirstOrDefaultAsync();
        }
    }
}
