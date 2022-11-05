using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Domain.Dto.Orders
{
    public  class UpdateOrderRequest
    {
        public int? NumeroPedido { get; set; }
        public string IndCancelado { get; set; }
        public string IndConcluido { get; set; }
    }
}
