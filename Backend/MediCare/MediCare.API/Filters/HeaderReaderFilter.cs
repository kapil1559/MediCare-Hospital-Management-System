//using MediCare.API.Helpers;
using MediCare.Core.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace MediCare.API.Filters;

public class HeaderReaderFilter : IActionFilter
{
    private readonly HeaderContext _headerContext;

    public HeaderReaderFilter(HeaderContext headerContext)
    {
        _headerContext = headerContext;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var headers = context.HttpContext.Request.Headers;

        if (!int.TryParse(headers["HospitalID"], out var hospitalId) ||
            !int.TryParse(headers["LocationID"], out var locationId) ||
            !int.TryParse(headers["UserID"], out var userId))
        {
            context.Result = new BadRequestObjectResult(
                "HospitalID, LocationID and UserID headers are required and must be valid integers.");

            return;
        }

        _headerContext.HospitalID = hospitalId;
        _headerContext.LocationID = locationId;
        _headerContext.UserID = userId;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}