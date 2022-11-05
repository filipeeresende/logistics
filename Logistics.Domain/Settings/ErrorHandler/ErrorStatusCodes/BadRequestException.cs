using System.Collections.Generic;
using System.Net;

namespace Logistics.Domain.Settings.ErrorHandler.ErrorStatusCodes
{
    public class BadRequestException : HttpException
    {
        private static readonly HttpStatusCode _statusCode = HttpStatusCode.BadRequest;

        public BadRequestException(string message) : base(message, _statusCode)
        {
        }

        public BadRequestException(IList<string> messages) : base(messages, _statusCode)
        {
        }
    }
}
