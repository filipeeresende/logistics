using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Tests.Repositories
{
    public static class PedidoRepositoryMock
    {

        public static OrderResponse GetOrderByIdMock()
        {
            return new OrderResponse
            {
                NumeroPedido = 139,
                HoraPedido = DateTime.Now,
                IndCancelado = false,
                IndConcluido = false
            };
        }
        public static OrderResponse GetOrderByIdIsIncompletedMock()
        {
            return new OrderResponse
            {
                NumeroPedido = 139,
                HoraPedido = DateTime.Now,
                IndCancelado = true,
                IndConcluido = false
            };
        }
        public static IList<OrdersResponse> GetOrdersMock()
        {
            return new List<OrdersResponse>
            {
                new OrdersResponse
                {
                    NumeroPedido = 139,
                    HoraPedido = DateTime.Now,
                    IndCancelado = false,
                    IndConcluido = true
                },
                new OrdersResponse
                {
                    NumeroPedido = 150,
                    HoraPedido = DateTime.Now,
                    IndCancelado = true,
                    IndConcluido = false
                }
            };
        }
        public static InsertOrderRequest InsertOrderMock()
        {
            return new InsertOrderRequest
            {
                NumeroPedido = 139,
                HoraPedido = DateTime.Now
            };
        }

        public static Pedido GetOrderByIdObjectMock()
        {
            return new Pedido
            {
                Id = 3
            };
        }
        public static bool CheckIfOrderExistsMock()
        {
            return true;
        }
        public static OrderResponse GetOrderByIdIncompletedMock()
        {
            return new OrderResponse
            {
                NumeroPedido = 139,
                HoraPedido = DateTime.Now,
                IndCancelado = true,
                IndConcluido = false
            };
        }
        public static OrderResponse GetOrderByIdCompletedMock()
        {
            return new OrderResponse
            {
                NumeroPedido = 139,
                HoraPedido = DateTime.Now,
                IndCancelado = false,
                IndConcluido = true
            };
        }
        public static UpdateOrderRequest UpdatedOrder()
        {
            return new UpdateOrderRequest
            {
                IndCancelado = false,
                IndConcluido = true
            };
        }
    }
}

