using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Permission;

namespace TopLearn.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private int _permissionId;
        private IPermissionService _permissionService;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                _permissionService = context.HttpContext.RequestServices
                    .GetService(typeof(IPermissionService)) as IPermissionService;

                string _userName = context.HttpContext.User.Identity.Name;
                if (!_permissionService.UserHavePermission(_permissionId, _userName))
                {
                    context.Result = new RedirectResult("/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
