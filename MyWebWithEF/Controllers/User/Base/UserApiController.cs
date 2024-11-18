using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebWithEF.Attributes;
using MyWebWithEF.BLL.Enums;

namespace MyWebWithEF.Controllers.User.Base
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/user/api/[controller]")]
    [CustomAuthorize(UserRole.User)]
    public abstract class UserApiController : ControllerBase
    {
    }
}
