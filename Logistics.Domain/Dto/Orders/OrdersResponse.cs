using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Domain.Dto.Orders
{
    public class OrdersResponse
    {
        public int Id { get; set; }
        public int? NumeroPedido { get; set; }
        public DateTime? HoraPedido { get; set; }
        public bool IndCancelado { get; set; }
        public bool IndConcluido { get; set; }
    }
}
