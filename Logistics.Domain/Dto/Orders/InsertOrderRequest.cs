﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Domain.Dto.Orders
{
    public  class InsertOrderRequest
    {
        public int? NumeroPedido { get; set; }
        public DateTime? HoraPedido { get; set; }

    }
}
