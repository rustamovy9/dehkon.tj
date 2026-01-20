using MainApp.HelpersApi.ApiConventions;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[ApiConventionType(typeof(CustomApiConventions))]
public class BaseController : ControllerBase
{
    protected string? GetErrorMessage
    {
        get
        {
            return ModelState.IsValid
                ? null
                : string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
        }
    }
}