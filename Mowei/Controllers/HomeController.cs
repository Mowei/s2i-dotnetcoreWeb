using Digipolis.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Mowei.Entities.DbContext;
using Mowei.Entities.Models;
using Mowei.ViewModels;

namespace Mowei.Controllers
{
    public class HomeController :  BaseController
    {
        
        public HomeController(IServiceProvider serviceProvider,
            IUowProvider uowProvider,
            SignInManager<ApplicationUser> signInManager) : base(serviceProvider,uowProvider, signInManager)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult UOWTEST()
        {
            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Project>();
                repository.Add(new Project { Name = "TEST" });
                uow.SaveChanges();
                return Json(repository.GetAll());
            }
        }
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
