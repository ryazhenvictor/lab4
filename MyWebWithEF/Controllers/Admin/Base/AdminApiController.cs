using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebWithEF.Attributes;
using MyWebWithEF.BLL.Enums;

namespace MyWebWithEF.Controllers.Admin.Base
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/admin/api/[controller]")]
    [CustomAuthorize(UserRole.Admin)]
    public abstract class AdminApiController : ControllerBase
    {
    }
}
