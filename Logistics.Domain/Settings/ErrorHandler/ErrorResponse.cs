using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Logistics.Domain.Settings.ErrorHandler
{
    public class ErrorResponse
    {
        public ErrorResponse(IList<string> errors)
        {
            Errors = errors;
        }
        public ErrorResponse(string error)
        {
            Errors = new string[] { error };
        }
        public ErrorResponse(ModelStateDictionary modelState)
        {
            Errors = GetModelStateErrors(modelState);
        }

        public IList<string> Errors { get; set; }

        private IList<string> GetModelStateErrors(ModelStateDictionary modelState)
        {
            List<string> responseErrors = new List<string>();

            var modelStateErrors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in modelStateErrors)
            {
                string errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                responseErrors.Add(errorMessage);
            }

            return responseErrors;
        }
    }
}
