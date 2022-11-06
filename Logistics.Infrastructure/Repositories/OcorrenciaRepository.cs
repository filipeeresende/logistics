using Logistics.Domain.Dto.Ocurrences;
using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Repositories
{
    public class OcorrenciaRepository : BaseRepository<Ocorrencia>, IOcorrenciaRepository
    {
        public OcorrenciaRepository(LogisticsContext context) : base(context)
        {
        }

        public async Task<OccurrenceResponse> GetOccurrenceById(int id)
        {
            return await _context.Ocorrencia
                .Where(x => x.Id == id)
                .Select(x => new OccurrenceResponse
                {
                    TipoOcorrencia = x.TipoOcorrencia,
                    IndFinalizadora = x.IndFinalizadora,
                    HoraOcorrencia = x.HoraOcorrencia,
                    IdPedido = x.IdPedido
                }).FirstOrDefaultAsync();
        }
        public async Task<IList<OccurrencesResponse>> GetOccurrences()
        {
            return await _context.Ocorrencia
              .Select(x => new OccurrencesResponse
              {
                  Id =  x.Id,
                  TipoOcorrencia = x.TipoOcorrencia,
                  IndFinalizadora = x.IndFinalizadora,
                  HoraOcorrencia = x.HoraOcorrencia,
                  IdPedido = x.IdPedido
              }).ToListAsync();
        }
        public async Task<Ocorrencia> GetOccurrenceByType(string occurrenceType)
        {
            return await _context.Ocorrencia
                .Where(x => x.TipoOcorrencia == occurrenceType)
                .Select(x => new Ocorrencia
                {
                   HoraOcorrencia = x.HoraOcorrencia,
                   Id = x.Id,
                }).OrderBy(x=> x.Id)
                .LastOrDefaultAsync();

        }
        public async Task<Ocorrencia> GetOccurrenceByIdObject(int id)
        {
            return await _context.Ocorrencia
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<Ocorrencia> GetOccurrenceByIdOrder(int id)
        {
            return await _context.Ocorrencia
                .Where(x => x.IdPedido == id)
                .FirstOrDefaultAsync();
        }
    }
}
