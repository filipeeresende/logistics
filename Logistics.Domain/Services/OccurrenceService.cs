using Logistics.Domain.Constants;
using Logistics.Domain.Dto;
using Logistics.Domain.Dto.Ocurrences;
using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.Domain.Settings.ErrorHandler.ErrorStatusCodes;
using Logistics.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Services
{
    public class OccurrenceService : IOccurrenceService
    {
        private readonly IOcorrenciaRepository _ocorrenciaRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IBaseRepository _baseRepository;

        public OccurrenceService(IOcorrenciaRepository ocorrenciaRepository, 
            IPedidoRepository pedidoRepository,
            IBaseRepository baseRepository) 
        {
            _ocorrenciaRepository = ocorrenciaRepository;
            _pedidoRepository = pedidoRepository;
            _baseRepository = baseRepository;
        }

        public async Task<OccurrenceResponse> GetOccurrenceById(int id)
        {
            OccurrenceResponse occurrence = await _ocorrenciaRepository.GetOccurrenceById(id);

            if (occurrence == null)
                throw new NotFoundException(ReturnMessageOccurrence.MessageOccurenceNotFound);

            return occurrence;
        }
        public async Task<IList<OccurrencesResponse>> GetOccurrences()
        {
            IList<OccurrencesResponse> ocurrences = await _ocorrenciaRepository.GetOccurrences();

            if (!ocurrences.Any())
                throw new NotFoundException(ReturnMessageOccurrence.MessageOccurencesNotFound);

            return ocurrences;
        }
        public async Task InsertOccurrence(OccurrenceRequest newOccurrence)
        {
            OrderResponse checkOrder = await _pedidoRepository.CheckIfOrderExists(newOccurrence.IdPedido);

            if (checkOrder == null)
                throw new NotFoundException(ReturnMessageOrder.MessageOrderNotFound);

            Ocorrencia occurrence = OcurrenceUtils.AddOccurrenceMapper(newOccurrence);

            await ValidateOccurenceType(occurrence);

            await ValidateIfOccurrenceIsFinisher(occurrence);

            await _baseRepository.InsertAsync(occurrence);

            if (occurrence.TipoOcorrencia == "entregue com sucesso")
                await _baseRepository.UpdateAsync(OrderUtils.OrderCompletedMapper(occurrence));

            if (occurrence.TipoOcorrencia == "cliente ausente" || occurrence.TipoOcorrencia == "avaria no produto")
                await _baseRepository.UpdateAsync(OrderUtils.OrderCanceled(occurrence));
        }
        public async Task DeleteOccurrence(int id)
        {
            await ValidateOccurrenceStatus(id);
            await _baseRepository.DeleteAsync(OcurrenceUtils.OccurenceDeleteMapper(id));
        }
        private async Task ValidateOccurenceType(Ocorrencia ocurrenceType)
        {
            Ocorrencia ocurrence = await _ocorrenciaRepository.GetOccurrenceByType(ocurrenceType.TipoOcorrencia);

            if (ocurrence != null) 
            {
                if (ocurrenceType.HoraOcorrencia.Subtract(ocurrence.HoraOcorrencia).TotalMinutes < 10)
                    throw new BadRequestException(ReturnMessageOccurrence.MessageOccurenceType);
            }               
        }
        private async Task ValidateIfOccurrenceIsFinisher(Ocorrencia newOccurrence)
        {
            OccurrenceResponse ocurrence = await _ocorrenciaRepository.GetOccurrenceById(newOccurrence.IdPedido);

            if (ocurrence != null)
                newOccurrence.IndFinalizadora = true;
        }
        private async Task ValidateOccurrenceStatus(int id)
        {
            OccurrenceResponse occurrence = await _ocorrenciaRepository.GetOccurrenceById(id);

            OrderResponse order = await _pedidoRepository.GetOrderById(occurrence.IdPedido);

            if (order.IndCancelado || order.IndConcluido)
                throw new NotFoundException(ReturnMessageOccurrence.MessageOccurrenceStatus);
        }

    }
}
