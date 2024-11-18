using Microsoft.AspNetCore.Authorization;
using MyWebWithEF.BLL.Enums;

namespace MyWebWithEF.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(string policy) : base(policy)
        {
        }

        public CustomAuthorizeAttribute(UserRole role) : base()
        {
            base.Roles = role.ToString();
        }
    }
}
