using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Domain.Dto.Ocurrences
{
    public class OccurrenceResponse
    {
        public string TipoOcorrencia { get; set; }
        public bool IndFinalizadora { get; set; }
        public DateTime? HoraOcorrencia { get; set; }
        public int IdPedido { get; set; }
    }
}
