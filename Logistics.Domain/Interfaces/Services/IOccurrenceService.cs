using Logistics.Domain.Dto.Occurrences;
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
        Task <string>InsertOccurrence(OccurrenceRequest newOccurrence);
        Task <string>DeleteOccurrence(int id);
        Task<string> UpdateOccurrence(UpdateOccurrenceRequest updateOccurrenceRequest, int id);
    }
}
