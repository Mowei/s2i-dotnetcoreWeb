using Digipolis.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Mowei.Entities.Models;

namespace Mowei.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IUowProvider _uowProvider;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        public BaseController(IServiceProvider serviceProvider,
            IUowProvider uowProvider,
            SignInManager<ApplicationUser> signInManager)
        {
            _serviceProvider = serviceProvider;
            _uowProvider = uowProvider;
            _signInManager = signInManager;
            _userManager = signInManager.UserManager;
        }
        #region 檢查重複登入
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                //檢查 ValidateSecurityStamp
                var user = await _signInManager.ValidateSecurityStampAsync(HttpContext.User);
                if (user == null)
                {
                    //踢除使用者
                    await _signInManager.SignOutAsync();
                    context.Result = RedirectToAction("UserLoggedOnWhere", "Account");
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }
        #endregion

        #region Helpers

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        protected IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        protected Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        protected List<Claim> GetUserRoles()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            return roles;
        }
        #endregion
    }
}
