using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Domain.Dto.Orders
{
    public  class UpdateOrderRequest
    {
        public bool IndCancelado { get; set; }
        public bool IndConcluido { get; set; }
    }
}
