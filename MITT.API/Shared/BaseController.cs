using Microsoft.AspNetCore.Mvc;

namespace MITT.API.Shared
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string GetUserId() => HttpContext.User.FindFirst("ID")?.Value ?? "0";

        protected int GetUserType() => int.Parse(HttpContext.User.FindFirst("DeveloperType")?.Value ?? "0");
    }
}