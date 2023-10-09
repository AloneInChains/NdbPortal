using Microsoft.AspNetCore.Mvc;
using NdbPortal.Web.Contracts;
using NdbPortal.Entities.Dtos.Login;
using NdbPortal.Web.exceptions;

namespace NdbPortal.Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly IWebApiClient _webApiClient;

        public LoginController(IWebApiClient webApiClient)
        {
            _webApiClient = webApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(LoginInfo loginInfo)
        {

            try
            {
                if (loginInfo != null)
                {
                    string token = await _webApiClient.AuthenticateAsync(loginInfo);

                    if (!string.IsNullOrEmpty(token))
                    {
                        HttpContext.Session.SetString("JWToken", token);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["AuthMessage"] = "Invalid credentials";
                        return Redirect("~/Login/Index");
                    }
                }
                else
                {
                    throw new AuthenticationException();
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }

        }

    }
}
