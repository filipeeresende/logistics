using Logistics.Domain.Dto.Ocurrences;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Domain.Utils
{
    public static class OcurrenceUtils
    {
        public static Ocorrencia AddOccurrenceMapper(OccurrenceRequest occurrence)
        {
            return new Ocorrencia
            {
                TipoOcorrencia = occurrence.TipoOcorrencia,
                HoraOcorrencia = occurrence.HoraOcorrencia,
                IndFinalizadora = false,
                IdPedido = occurrence.IdPedido,
            };
        }
     
        public static Ocorrencia OccurenceDeleteMapper(int id)
        {
            return new Ocorrencia
            {
                Id = id
              
            };
        }
    }
}
