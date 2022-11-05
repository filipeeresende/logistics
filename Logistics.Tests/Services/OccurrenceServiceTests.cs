using Logistics.Domain.Constants;
using Logistics.Domain.Dto.Ocurrences;
using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.Domain.Services;
using Logistics.Domain.Settings.ErrorHandler.ErrorStatusCodes;
using Logistics.Tests.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Logistics.Tests.Services
{
    public class OccurrenceServiceTests
    {
        private readonly Mock<IOcorrenciaRepository> _ocorrenciaRepository;
        private readonly Mock<IPedidoRepository> _pedidoRepository;
        private readonly Mock<IBaseRepository> _baseRepository;
        private readonly IOccurrenceService _ocorrenciaService;
        public OccurrenceServiceTests()
        {
            _ocorrenciaRepository = new Mock<IOcorrenciaRepository>();
            _pedidoRepository = new Mock<IPedidoRepository>();
            _baseRepository = new Mock<IBaseRepository>();
            _ocorrenciaService = new OccurrenceService(_ocorrenciaRepository.Object,
                                                        _pedidoRepository.Object,
                                                        _baseRepository.Object);
        }

        [Fact]
        public async Task GetOccurenceById_WhenTheOccurrenceIsFound_Success()
        {
            OccurrenceResponse occurrence = OcorrenciaRepositoryMock.GetOccurrenceByIdMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceById(It.IsAny<int>()))
                .ReturnsAsync(occurrence);

            OccurrenceResponse response = await _ocorrenciaService
                .GetOccurrenceById(It.IsAny<int>());

            Assert.Equal(occurrence, response);
        }
        [Fact]
        public async Task GetOccurenceById_WhenOrderNotFound_Error()
        {
            Task act() => _ocorrenciaService.GetOccurrenceById(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
        }
        [Fact]
        public async Task GetOccurences_WhenTheOccurrencesIsFound_Success()
        {
            IList<OccurrencesResponse> occurrences = OcorrenciaRepositoryMock.GetOccurrencesMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrences())
                .ReturnsAsync(occurrences);

            IList<OccurrencesResponse> response = await _ocorrenciaService
                .GetOccurrences();

            Assert.Equal(occurrences, response);
        }
        [Fact]
        public async Task GetOccurences_WhenOccurrencesNotFound_Error()
        {
            _ocorrenciaRepository.Setup(x => x.GetOccurrences())
           .ReturnsAsync(new List<OccurrencesResponse>());

            Task act() => _ocorrenciaService.GetOccurrenceById(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task InsertOccurrence_WhenOrderIsNotFound_Error()
        {
            Task act() => _ocorrenciaService.InsertOccurrence(new OccurrenceRequest());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
        }
        [Fact]
        public async Task InsertOccurence_WhenOccurrenceIsOfTheSameTypeAndIsLessThan10Minutes_Error()
        {
            OrderResponse occurrence = PedidoRepositoryMock.CheckIfOrderExistsMock();

            _pedidoRepository.Setup(x => x.CheckIfOrderExists(It.IsAny<int>()))
                .ReturnsAsync(occurrence);

            OccurrenceRequest newOccurence = OcorrenciaRepositoryMock.RequestOccurrenceMock();

            Ocorrencia typeOccurrence = OcorrenciaRepositoryMock.GetOccurrenceByTypeMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceByType(It.IsAny<string>()))
                .ReturnsAsync(typeOccurrence);

            Task act() => _ocorrenciaService.InsertOccurrence(newOccurence);

            BadRequestException exception = await Assert.ThrowsAsync<BadRequestException>(act);
        }
        [Fact]
        public async Task InsertOccurence_WhenOccurrenceIsFinisher_Sucess()
        {
            OrderResponse order = PedidoRepositoryMock.CheckIfOrderExistsMock();

            _pedidoRepository.Setup(x => x.CheckIfOrderExists(It.IsAny<int>()))
                .ReturnsAsync(order);

            OccurrenceRequest newOccurence = OcorrenciaRepositoryMock.InsertRequestOccurrenceMock();

            Ocorrencia typeOccurrence = OcorrenciaRepositoryMock.GetOccurrenceByTypeMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceByType(It.IsAny<string>()))
                .ReturnsAsync(typeOccurrence);

            OccurrenceResponse occurrence = OcorrenciaRepositoryMock.GetOccurrenceByIdMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceById(It.IsAny<int>()))
               .ReturnsAsync(occurrence);

            _baseRepository.Setup(x => x.DeleteAsync(It.IsAny<Ocorrencia>()));

            await _ocorrenciaService.InsertOccurrence(newOccurence);
        }

        [Fact]
        public async Task InsertOccurence_WhenOccurrenceIsFinisherIsStatusCompleted_Sucess()
        {
            OrderResponse order = PedidoRepositoryMock.CheckIfOrderExistsMock();

            _pedidoRepository.Setup(x => x.CheckIfOrderExists(It.IsAny<int>()))
                .ReturnsAsync(order);

            OccurrenceRequest newOccurence = OcorrenciaRepositoryMock.InsertRequestOccurrenceMock();

            Ocorrencia typeOccurrence = OcorrenciaRepositoryMock.GetOccurrenceByTypeMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceByType(It.IsAny<string>()))
                .ReturnsAsync(typeOccurrence);

            OccurrenceResponse occurrence = OcorrenciaRepositoryMock.GetOccurrenceByIdMock();

            _ocorrenciaRepository.Setup(x => x.GetOccurrenceById(It.IsAny<int>()))
               .ReturnsAsync(occurrence);

            _baseRepository.Setup(x => x.InsertAsync(It.IsAny<Ocorrencia>()));

            await _ocorrenciaService.InsertOccurrence(newOccurence);
        }
        [Fact]
        public async Task DeleteOccurence_WhenTheOrderstatusIsCompleted_Error()
        {
            OccurrenceResponse occurrence = OcorrenciaRepositoryMock.GetOccurrenceByIdCompleteMock();
            _ocorrenciaRepository.Setup(x => x.GetOccurrenceById(It.IsAny<int>())) 
               .ReturnsAsync(occurrence);

            OrderResponse order = PedidoRepositoryMock.GetOrderByIdCompletedMock();
            _pedidoRepository.Setup(x => x.GetOrderById(It.IsAny<int>()))
             .ReturnsAsync(order);

            Task act() => _ocorrenciaService.DeleteOccurrence(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);


        }
        [Fact]
        public async Task DeleteOccurence_WhenTheOrderstatusIsIncompleted_Error()
        {
            OccurrenceResponse occurrence = OcorrenciaRepositoryMock.GetOccurrenceByIdIncompletedMock();
            _ocorrenciaRepository.Setup(x => x.GetOccurrenceById(It.IsAny<int>()))
               .ReturnsAsync(occurrence);

            OrderResponse order = PedidoRepositoryMock.GetOrderByIdIncompletedMock();
            _pedidoRepository.Setup(x => x.GetOrderById(It.IsAny<int>()))
             .ReturnsAsync(order);

            Task act() => _ocorrenciaService.DeleteOccurrence(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);

        }
        [Fact]
        public async Task DeleteOccurence_WhenOccurenceIsDeleted_Sucess()
        {
            OccurrenceResponse occurrence = OcorrenciaRepositoryMock.GetOccurrenceByIdMock();
            _ocorrenciaRepository.Setup(x => x.GetOccurrenceById(It.IsAny<int>()))
               .ReturnsAsync(occurrence);

            OrderResponse order = PedidoRepositoryMock.GetOrderByIdMock();
            _pedidoRepository.Setup(x => x.GetOrderById(It.IsAny<int>()))
             .ReturnsAsync(order);

            _baseRepository.Setup(x => x.DeleteAsync(It.IsAny<Ocorrencia>()));

            await _ocorrenciaService.DeleteOccurrence(It.IsAny<int>());

        }
    }
}
