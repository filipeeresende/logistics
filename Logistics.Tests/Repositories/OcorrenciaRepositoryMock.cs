using Logistics.Domain.Dto.Occurrences;
using Logistics.Domain.Dto.Ocurrences;
using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Tests.Repositories
{
    public static class OcorrenciaRepositoryMock
    {
        public static OccurrenceResponse GetOccurrenceByIdMock()
        {
            return new OccurrenceResponse
            {
                TipoOcorrencia = "Em rota de entrega",
                IndFinalizadora = false,
                HoraOcorrencia = new DateTime(2022, 11, 06, 13, 12, 00),
                IdPedido = 240
            };
        }
        public static IList<OccurrencesResponse> GetOccurrencesMock()
        {
            return new List<OccurrencesResponse>
            {
                new OccurrencesResponse
                {
                     Id = 1,
                     TipoOcorrencia = "entregue com sucesso",
                     IndFinalizadora = true,
                     HoraOcorrencia = DateTime.Now,
                     IdPedido = 240
                },
                new OccurrencesResponse
                {
                     TipoOcorrencia = "Em rota de entrega",
                     IndFinalizadora = false,
                     HoraOcorrencia = DateTime.Now,
                     IdPedido = 240
                }

            };

        }
        public static OccurrenceRequest RequestOccurrenceMock()
        {
            return new OccurrenceRequest
            {
                TipoOcorrencia = "entregue com sucesso",
                HoraOcorrencia = new DateTime(2022, 11, 06, 13, 12, 00),
                IdPedido = 240
            };
        }
        public static Ocorrencia GetOccurrenceByTypeMock()
        {
            return new Ocorrencia
            {
                HoraOcorrencia = new DateTime(2022, 11, 06, 13, 11, 00),
            };
        }
        public static OccurrenceRequest InsertRequestOccurrenceMock()
        {
            return new OccurrenceRequest
            {
                TipoOcorrencia = "entregue com sucesso",
                HoraOcorrencia = new DateTime(2022, 11, 07, 13, 12, 00),
                IdPedido = 240
            };
        }
        public static Ocorrencia InsertAsyncMock()
        {
            return new Ocorrencia
            {
                TipoOcorrencia = "entregue com sucesso",
                HoraOcorrencia = new DateTime(2022, 11, 07, 13, 12, 00),
                IndFinalizadora = false,
                IdPedido = 240
            };
        }
        public static OccurrenceResponse GetOccurrenceByIdIncompletedMock()
        {
            return new OccurrenceResponse
            {
                TipoOcorrencia = "cliente ausente",
                IndFinalizadora = false,
                HoraOcorrencia = new DateTime(2022, 11, 06, 13, 12, 00),
                IdPedido = 250
            };
        }
        public static OccurrenceResponse GetOccurrenceByIdCompleteMock()
        {
            return new OccurrenceResponse
            {
                TipoOcorrencia = "entregue com sucesso",
                IndFinalizadora = false,
                HoraOcorrencia = new DateTime(2022, 11, 06, 13, 12, 00),
                IdPedido = 260
            };
        }
        public static Ocorrencia GetOccurrenceByIdOrderMock()
        {
            return new Ocorrencia
            {
                TipoOcorrencia = "a caminho",
                HoraOcorrencia = new DateTime(2022, 11, 07, 13, 12, 00),
                IdPedido = 240
            };
        }
        public static Ocorrencia GetOccurrenceByIdObjectMock()
        {
            return new Ocorrencia
            {
                TipoOcorrencia = "a caminho",
                HoraOcorrencia = new DateTime(2022, 11, 07, 13, 12, 00),
                IdPedido = 240
            };
        }
        public static UpdateOccurrenceRequest UpdatedOccurrenceRequest()
        {
            return new UpdateOccurrenceRequest
            {
                TipoOcorrencia = "entregue com sucesso",
            };
        }
    }
}

