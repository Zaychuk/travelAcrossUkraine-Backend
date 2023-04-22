using Microsoft.AspNetCore.Mvc;
using TravelAcrossUkraine.WebApi.Exceptions;

namespace TravelAcrossUkraine.WebApi.Utility;

public class ExceptionHandler
{
    public static ObjectResult Handle<T>(Exception ex, ILogger<T> logger)
    {
        logger.LogError(ex.Message);

        return ex switch
        {
            BadHttpRequestException => HandleBadRequestException(ex),
            ForbiddenException => HandleForbiddenException(ex),
            NotFoundException => HandleNotFoundException(ex),
            ArgumentException => HandleArgumentException(ex),
            _ => HandleUndefinedException(ex),
        };
    }

    private static ObjectResult HandleBadRequestException(Exception ex)
    {
        return new BadRequestObjectResult(ex.Message) { StatusCode = StatusCodes.Status400BadRequest };
    }

    private static ObjectResult HandleForbiddenException(Exception ex)
    {
        return new ObjectResult(ex.Message) { StatusCode = StatusCodes.Status403Forbidden };
    }

    private static ObjectResult HandleNotFoundException(Exception ex)
    {
        return new NotFoundObjectResult(ex.Message) { StatusCode = StatusCodes.Status404NotFound };
    }

    private static ObjectResult HandleArgumentException(Exception ex)
    {
        return new ObjectResult(ex.Message) { StatusCode = StatusCodes.Status400BadRequest };
    }

    private static ObjectResult HandleUndefinedException(Exception ex)
    {
        return new ObjectResult($"{ErrorMessages.UnexpectedError}. Message: {ex.Message} Info:{ex}") { StatusCode = StatusCodes.Status500InternalServerError };
    }
}
