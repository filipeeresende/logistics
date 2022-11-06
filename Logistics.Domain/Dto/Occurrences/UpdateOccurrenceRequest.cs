using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Domain.Dto.Occurrences
{
    public class UpdateOccurrenceRequest
    {
        public string TipoOcorrencia { get; set; }
        public int IdPedido { get; set; }
    }
}

