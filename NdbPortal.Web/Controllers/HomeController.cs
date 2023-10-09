using Microsoft.AspNetCore.Mvc;
using NdbPortal.Entities.Models;
using NdbPortal.Web.Authorization;
using NdbPortal.Web.Contracts;
using NdbPortal.Web.Models;
using System.Diagnostics;

namespace NdbPortal.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebApiClient _webApiClient;


        public HomeController(ILogger<HomeController> logger, IWebApiClient webApiClient)
        {
            _logger = logger;
            _webApiClient = webApiClient;
        }

        public async Task<IActionResult> Index()
        {
            Employee? employee = (Employee?)HttpContext.Items["User"];
            var token = HttpContext.Session.GetString("JWToken");

            ViewBag.EmployeeName = $"{employee.Name} {employee.Surname}";

            var normativeDocuments = await _webApiClient.GetNormativeDocumentsByEmployeeIdAsync(token, employee.Id);

            return View(normativeDocuments);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid normativeDocumentId)
        {

            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                if (token == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                await _webApiClient.DeleteRecordAsync("NormativeDocuments", normativeDocumentId, token);
            }
            catch (ConfictDbDeletionException ex)
            {
                _logger.LogError(ex, $"Error occured deleting document (id = {normativeDocumentId}");
                TempData["DeletionError"] = "Error occured while deleting document!\nPlease delete related records first.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured deleting document (id = {normativeDocumentId}");
                TempData["DeletionError"] = "Unknown error deleting document";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("JWToken", "");
            HttpContext.Items["User"] = null;
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Search(string searchValue)
        {
            Employee? employee = (Employee?)HttpContext.Items["User"];
            var token = HttpContext.Session.GetString("JWToken");

            if (employee == null || token == null)
                return RedirectToAction("Index", "Login");

            if (string.IsNullOrEmpty(searchValue))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("GetSearch", new {q = searchValue});
        }

        [HttpGet("Search")]
        public async Task<IActionResult> GetSearch(string q)
        {
            Employee? employee = (Employee?)HttpContext.Items["User"];
            var token = HttpContext.Session.GetString("JWToken");

            if (employee == null || token == null)
                return RedirectToAction("Index", "Login");

            ViewBag.EmployeeName = $"{employee.Name} {employee.Surname}";
            var normativeDocuments = await _webApiClient.GetNormativeDocumentsByEmployeeIdAsync(token, employee.Id);

            if (normativeDocuments != null)
            {
                if (DateTime.TryParse(q, out var searchDateTime))
                {
                    normativeDocuments = normativeDocuments.Where(x => x.CreatedOn.Date == searchDateTime.Date);
                }
                else
                {
                    normativeDocuments = normativeDocuments.Where(x => x.DocumentNumber.ToLower().Contains(q.Trim().ToLower()));
                }
            }

            ViewBag.BackToList = "Back to full list";
            return View("Index", normativeDocuments);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}