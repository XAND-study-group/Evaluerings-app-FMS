using Microsoft.AspNetCore.Http;
using Module.Shared.Models;

namespace Module.Shared.Extensions;

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