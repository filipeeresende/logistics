using Logistics.Application.v1.Controllers.Base;
using Logistics.Domain.Constants;
using Logistics.Domain.Dto;
using Logistics.Domain.Dto.Ocurrences;
using Logistics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Application.v1.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/occurrence/")]
    public class OccurrenceController : MainController
    {
        private readonly IOccurrenceService _occurrenceService;

        public OccurrenceController(IOccurrenceService occurrenceService)
        {
            _occurrenceService = occurrenceService;
        }

        [SwaggerOperation("Returns a data base occurence by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(OccurrenceResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOccurrence.MessageOccurenceNotFound, typeof(string))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOccurrenceById(int id)
        {
            return Ok(await _occurrenceService.GetOccurrenceById(id));
        }

        [SwaggerOperation("List all database occurence")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(IList<OccurrencesResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOccurrence.MessageOccurencesNotFound, typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetOccurrences()
        {
            return Ok(await _occurrenceService.GetOccurrences());
        }

        [SwaggerOperation("Insert new occurrence")]
        [SwaggerResponse(StatusCodes.Status200OK, ReturnMessageOccurrence.MessageInsertOcorrence, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOrder.MessageOrderNotFound, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOccurrence.MessageOccurenceType, typeof(string))]

        [HttpPost]
        public async Task<IActionResult> InsertOccurrence(OccurrenceRequest ocurrence)
        {
            await _occurrenceService.InsertOccurrence(ocurrence);
            return Ok(ReturnMessageOccurrence.MessageInsertOcorrence);
        }

        [SwaggerOperation("Delete an order by id")]
        [SwaggerResponse(StatusCodes.Status200OK, ReturnMessageOccurrence.MessageDeleteOccurrence, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOccurrence.MessageOccurrenceStatus, typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOccurrence(int id)
        {
            await _occurrenceService.DeleteOccurrence(id);
            return Ok(ReturnMessageOccurrence.MessageDeleteOccurrence);
        }
    }
}
