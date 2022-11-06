using Logistics.Domain.Constants;
using Logistics.Domain.Dto;
using Logistics.Domain.Dto.Occurrences;
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
        public OccurrenceService(IOcorrenciaRepository ocorrenciaRepository, 
                                 IPedidoRepository pedidoRepository) 
        {
            _ocorrenciaRepository = ocorrenciaRepository;
            _pedidoRepository = pedidoRepository;
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
        public async Task<string> InsertOccurrence(OccurrenceRequest newOccurrence)
        {
            if (!await _pedidoRepository.CheckIfOrderExists(newOccurrence.IdPedido))
                throw new NotFoundException(ReturnMessageOrder.MessageOrderNotFound);

            Ocorrencia occurrence = OcurrenceUtils.AddOccurrenceMapper(newOccurrence);

            await ValidateOccurenceType(occurrence);

            await ValidateIfOccurrenceIsFinisher(occurrence);

            await _ocorrenciaRepository.InsertAsync(occurrence);

            await ValidateIfTheOrderWasCompletedOrCanceled(occurrence);

            return ReturnMessageOccurrence.MessageInsertOcorrence;

        }
        public async Task<string> DeleteOccurrence(int id)
        {
            Ocorrencia occurrence = await _ocorrenciaRepository.GetOccurrenceByIdObject(id);

            if (occurrence == null)
                throw new NotFoundException(ReturnMessageOccurrence.MessageOccurenceNotFound);

            await ValidateOccurrenceStatus(occurrence.IdPedido);

            await _ocorrenciaRepository.DeleteAsync(OcurrenceUtils.OccurenceDeleteMapper(id));

            return ReturnMessageOccurrence.MessageDeleteOccurrence;
        }
        public async Task <string>UpdateOccurrence(UpdateOccurrenceRequest updateOccurrenceRequest, int id)
        {

            if (!await _pedidoRepository.CheckIfOrderExists(updateOccurrenceRequest.IdPedido))
                throw new NotFoundException(ReturnMessageOrder.MessageOrderNotFound);

            Ocorrencia occurrence = await _ocorrenciaRepository.GetOccurrenceByIdObject(id);

            if (occurrence == null)
                throw new NotFoundException(ReturnMessageOccurrence.MessageOccurenceNotFound);

            occurrence.TipoOcorrencia = updateOccurrenceRequest.TipoOcorrencia;

            await ValidateOccurenceType(occurrence);

            await ValidateIfTheOrderWasCompletedOrCanceled(occurrence);

            await _ocorrenciaRepository.UpdateAsync(occurrence);

            return ReturnMessageOccurrence.MessageUpdateOccurrence;

        }
        private async Task ValidateOccurenceType(Ocorrencia ocurrenceType)
        {
            Ocorrencia ocurrence = await _ocorrenciaRepository
                .GetOccurrenceByType(ocurrenceType.TipoOcorrencia);

            if (ocurrence != null) 
            {
                if (ocurrenceType.HoraOcorrencia.Subtract(ocurrence.HoraOcorrencia).TotalMinutes < 10)
                    throw new BadRequestException(ReturnMessageOccurrence.MessageOccurenceType);
            }               
        }
        private async Task ValidateIfOccurrenceIsFinisher(Ocorrencia newOccurrence)
        {
            Ocorrencia ocurrence = await _ocorrenciaRepository
                .GetOccurrenceByIdOrder(newOccurrence.IdPedido);

            if (ocurrence != null)
                newOccurrence.IndFinalizadora = true;
        }
        private async Task ValidateOccurrenceStatus(int idPedido)
        {
            OrderResponse order = await _pedidoRepository.GetOrderById(idPedido);

            if (order.IndCancelado || order.IndConcluido)
                throw new NotFoundException(ReturnMessageOccurrence.MessageOccurrenceStatus);
        }
        private async Task ValidateIfTheOrderWasCompletedOrCanceled(Ocorrencia occurrence)
        {
            if (occurrence.TipoOcorrencia == Validations.ValidationOrderDevileverd)
                await _pedidoRepository.UpdateAsync(OrderUtils.OrderCompletedMapper(occurrence));

            if (occurrence.TipoOcorrencia == Validations.ValidationProductMalfunction || 
                occurrence.TipoOcorrencia == Validations.ValidationAbsentCustomer)
                await _pedidoRepository.UpdateAsync(OrderUtils.OrderCanceled(occurrence));
        } 
    }
}
