using Microsoft.AspNetCore.Http;

namespace SharedKernel.Models.Extensions;

public static class IResultExtension
{
    public static IResult ReturnHttpResult<TResponse>(this Result<TResponse> result)
    {
        return result.Status switch
        {
            ResultStatus.Error => Results.BadRequest(result),
            ResultStatus.Warning => Results.NotFound(result),
            _ => Results.Ok(result)
        };
    }
}