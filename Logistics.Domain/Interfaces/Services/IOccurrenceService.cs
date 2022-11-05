using Logistics.Domain.Dto.Ocurrences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces.Services
{
    public interface IOccurrenceService
    {
        Task<OccurrenceResponse> GetOccurrenceById(int id);
        Task<IList<OccurrencesResponse>> GetOccurrences();
        Task InsertOccurrence(OccurrenceRequest newOccurrence);
        Task DeleteOccurrence(int id);
    }
}
