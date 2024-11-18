using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebWithEF.Attributes;
using MyWebWithEF.BLL.Enums;

namespace MyWebWithEF.Controllers.Public.Base
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]")]
    [AllowAnonymous]
    public abstract class PublicApiController : ControllerBase
    {
    }
}
