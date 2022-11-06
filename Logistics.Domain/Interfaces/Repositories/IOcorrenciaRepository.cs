using Logistics.Domain.Dto.Ocurrences;
using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IOcorrenciaRepository : IBaseRepository<Ocorrencia>
    {
        Task<OccurrenceResponse> GetOccurrenceById(int id);
        Task<IList<OccurrencesResponse>> GetOccurrences();
        Task<Ocorrencia> GetOccurrenceByType(string ocorrenceType);
        Task<Ocorrencia> GetOccurrenceByIdObject(int id);
        Task<Ocorrencia> GetOccurrenceByIdOrder(int id);

    }
}
