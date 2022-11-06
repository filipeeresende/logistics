using Logistics.Domain.Dto.Ocurrences;
using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Domain.Utils
{
    public static class OrderUtils
    {
        public static Pedido AddOrderMapper(InsertOrderRequest order)
        {
            return new Pedido
            {
                NumeroPedido = order.NumeroPedido,
                HoraPedido = order.HoraPedido,
            };
        }
        public static Pedido OrderCompletedMapper(Ocorrencia occurrence)
        {
            return new Pedido
            {
                Id = occurrence.IdPedido,
                IndConcluido = true
            };
        }
        public static Pedido OrderCanceled(Ocorrencia occurrence)
        {
            return new Pedido
            {
                Id = occurrence.IdPedido,
                IndCancelado = true
            };
        }
    }
}
