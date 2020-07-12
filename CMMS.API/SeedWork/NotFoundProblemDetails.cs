using CMMS.Application.Configuration.Validation;
using Microsoft.AspNetCore.Http;

namespace CMMS.API.SeedWork
{
    public class NotFoundProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public NotFoundProblemDetails(NotFoundException exception)
        {
            Title = exception.Message;
            Status = StatusCodes.Status404NotFound;
            Detail = exception.Details;
            Type = "https://somedomain/validation-error";
        }
    }
}
