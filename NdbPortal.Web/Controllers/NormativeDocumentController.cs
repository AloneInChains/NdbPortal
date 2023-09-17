using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using NdbPortal.Entities.Dtos.NormativeDocument;
using NdbPortal.Entities.Dtos.NormativeDocumentConfidentialityLevel;
using NdbPortal.Entities.Dtos.NormativeDocumentFile;
using NdbPortal.Entities.Dtos.NormativeDocumentRelation;
using NdbPortal.Entities.Dtos.NormativeDocumentRelationType;
using NdbPortal.Entities.Dtos.NormativeDocumentType;
using NdbPortal.Entities.Dtos.NormativeDocumentVisa;
using NdbPortal.Entities.Models;
using NdbPortal.Web.Contracts;
using NdbPortal.Web.Models;

namespace NdbPortal.Web.Controllers
{
    public class NormativeDocumentController : Controller
    {

        private readonly IWebApiClient _webApiClient;
        private readonly ILogger<NormativeDocumentController> _logger;

        public NormativeDocumentController(IWebApiClient webApiClient, ILogger<NormativeDocumentController> logger)
        {
            _webApiClient = webApiClient;
            _logger = logger;
        }

        // GET: NormativeDocumentController
        public async Task<IActionResult> Edit(Guid id)
        {

            NormativeDocumentGetWithDetailsDto? normativeDocument = new NormativeDocumentGetWithDetailsDto();

            Employee? employee = (Employee?)HttpContext.Items["User"];
            var token = HttpContext.Session.GetString("JWToken");

            if (employee == null || token == null)
                return RedirectToAction("Index", "Login");

            ViewBag.EmployeeName = $"{employee.Name} {employee.Surname}";
            

            IEnumerable<NormativeDocumentGetWithDetailsDto>? normativeDocumentList = 
                await _webApiClient.GetEntityRecordsAsync<IEnumerable<NormativeDocumentGetWithDetailsDto>?>("NormativeDocuments/WithDetails", token);

            if (normativeDocumentList == null)
            {
                return NotFound();
            }

            normativeDocument = normativeDocumentList.FirstOrDefault(x => x.Id == id);

            if (normativeDocument == null)
            {
                return NotFound();
            }

            var vm = new NormativeDocumentViewModel();
            vm.NormativeDocumentTypeList = await GetDocumentTypeItems();
            vm.NormativeDocumentConfLevelList = await GetDocumentConfLevelItems();
            vm.AvailableRelatedDocumentList = await GetAvailableRelatedDocumentListAsync(normativeDocument.Id);
            vm.AvailableRelationTypeList = await GetAvailableRelationTypesListAsync();
            vm.RelatedDocuments = await GetRelatedDocumentsAsync(normativeDocument.Id);
            vm.Approvals = await GetVisasAsync(normativeDocument.Id);
            vm.Versions = await GetVersions(normativeDocument.Id);
            vm.Files = await GetFilesAsync(normativeDocument.Id);

            vm.NormativeDocument = normativeDocument;

            return View(vm);

        }

        // GET: NormativeDocumentController/CreateVersion/5
        public async Task<IActionResult> CreateVersion(Guid mainDocumentId)
        {

            Employee? employee = (Employee?)HttpContext.Items["User"];
            var token = HttpContext.Session.GetString("JWToken");

            if (employee == null || token == null)
                return RedirectToAction("Index", "Login");

            ViewBag.EmployeeName = $"{employee.Name} {employee.Surname}";

            var vm = new NormativeDocumentViewModel();
            vm.NormativeDocumentTypeList = await GetDocumentTypeItems();
            vm.NormativeDocumentConfLevelList = await GetDocumentConfLevelItems();

            if (mainDocumentId == Guid.Empty)
            {
                return View("Create", vm);
            }

            vm.NormativeDocument = new NormativeDocumentGetWithDetailsDto();
            vm.NormativeDocument.MainDocumentId = mainDocumentId;
            vm.NormativeDocument.VersionNumber = (await GetNextVersionAsync(mainDocumentId)) + 1;

            return View("Create", vm);
        }

        // GET: NormativeDocumentController/Create
        public async Task<IActionResult> Create()
        {

            Employee? employee = (Employee?)HttpContext.Items["User"];
            var token = HttpContext.Session.GetString("JWToken");

            if (employee == null || token == null)
                return RedirectToAction("Index", "Login");

            ViewBag.EmployeeName = $"{employee.Name} {employee.Surname}";

            var vm = new NormativeDocumentViewModel();
            vm.NormativeDocumentTypeList = await GetDocumentTypeItems();
            vm.NormativeDocumentConfLevelList = await GetDocumentConfLevelItems();
            vm.NormativeDocument = new NormativeDocumentGetWithDetailsDto
            {
                VersionNumber = 1                
            };

            return View(vm);
        }

        // POST: NormativeDocumentController/Create
        [HttpPost]
        public async Task<IActionResult> Create(NormativeDocumentViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                vm.NormativeDocumentTypeList = await GetDocumentTypeItems();
                vm.NormativeDocumentConfLevelList = await GetDocumentConfLevelItems();
                return View(vm);
            }

            string? token = HttpContext.Session.GetString("JWToken");
            var employee = (Employee?)HttpContext.Items["User"];

            vm.NormativeDocument.Id = Guid.NewGuid();
            vm.NormativeDocument.CreatedOn = DateTime.Now;
            if (employee != null)
            {
                vm.NormativeDocument.CreatedById = employee.Id;
                vm.NormativeDocument.CompanyId = employee.CompanyId;
                vm.NormativeDocument.VersionNumber = vm.NormativeDocument.VersionNumber;
                vm.NormativeDocument.MainDocumentId = vm.NormativeDocument.MainDocumentId;
            }

            await _webApiClient.AddRecordAsync("NormativeDocuments", vm.NormativeDocument, token);

            return RedirectToAction("Index", "Home");
        }

        // POST: NormativeDocumentController/Update
        [HttpPost]
        public async Task<IActionResult> Update(NormativeDocumentViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", vm);
            }

            string? token = HttpContext.Session.GetString("JWToken");

            await _webApiClient.EditRecordAsync("NormativeDocuments", vm.NormativeDocument.Id, vm.NormativeDocument, token);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFile(Guid recordId, Guid normativeDocumentId)
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (token == null)
            {
                return RedirectToAction("Index", "Login");
            }

            try
            {
                await _webApiClient.DeleteRecordAsync("NormativeDocumentFiles", recordId, token);
            }
            catch (ConfictDbDeletionException ex)
            {
                _logger.LogError(ex, "Error deleting visa");
                TempData["DeletionError"] = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting visa");
                TempData["DeletionError"] = ex.Message;
            }
            return RedirectToAction("Edit", new { @Id = normativeDocumentId });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteVisa(Guid recordId, Guid normativeDocumentId)
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (token == null)
            {
                return RedirectToAction("Index", "Login");
            }

            try
            {
                await _webApiClient.DeleteRecordAsync("NormativeDocumentVisas", recordId, token);
            }
            catch(ConfictDbDeletionException ex)
            {
                _logger.LogError(ex, "Error deleting visa");
                TempData["DeletionError"] = ex.Message;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error deleting visa");
                TempData["DeletionError"] = ex.Message;
            }

            return RedirectToAction("Edit", new { @Id = normativeDocumentId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRelatedDocument(Guid recordId, Guid normativeDocumentId)
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (token == null)
            {
                return RedirectToAction("Index", "Login");
            }

            try
            {
                await _webApiClient.DeleteRecordAsync("NormativeDocumentRelations", recordId, token);
            }
            catch (ConfictDbDeletionException ex)
            {
                _logger.LogError(ex, "Error deleting visa");
                TempData["DeletionError"] = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting visa");
                TempData["DeletionError"] = ex.Message;
            }

            return RedirectToAction("Edit", new { @Id = normativeDocumentId });
        }

        [HttpPost]
        public async Task<IActionResult> AddRelatedDocument(Guid relationDocumentId, Guid relationTypeId, Guid relatedDocumentId)
        {

            string? token = HttpContext.Session.GetString("JWToken");

            NormativeDocumentRelationAddDto relation = new NormativeDocumentRelationAddDto
            {
                Id = Guid.NewGuid(),
                RelationDocumentId = relationDocumentId,
                RelatedDocumentId = relatedDocumentId,
                RelationTypeId = relationTypeId
            };

            await _webApiClient.AddRecordAsync("NormativeDocumentRelations", relation, token);

            var vm = new NormativeDocumentViewModel();
            vm.NormativeDocumentTypeList = await GetDocumentTypeItems();
            vm.NormativeDocumentConfLevelList = await GetDocumentConfLevelItems();
            vm.RelatedDocuments = await GetRelatedDocumentsAsync(relationDocumentId);
            vm.AvailableRelatedDocumentList = await GetAvailableRelatedDocumentListAsync(relationDocumentId);
            vm.AvailableRelationTypeList = await GetAvailableRelationTypesListAsync();
            var normativeDocuments = await _webApiClient.GetEntityRecordsAsync<IEnumerable<NormativeDocumentGetWithDetailsDto>?>("NormativeDocument", token);

            if (normativeDocuments != null)
            {
                vm.NormativeDocument = normativeDocuments.FirstOrDefault(x => x.Id == relationDocumentId);
            }

            return RedirectToAction("Edit", new { @Id = relationDocumentId });
        }

        [HttpPost]
        public async Task<IActionResult> AddApproval(Guid visaNormativeDocumentId, string visaComment, bool approveResult)
        {

            string? token = HttpContext.Session.GetString("JWToken");
            var employee = (Employee?)HttpContext.Items["User"];

            if (employee == null || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var visa = new NormativeDocumentVisaAddDto
            {
                Id = Guid.NewGuid(),
                ApproverId = employee.Id,
                CreatedOn = DateTime.Now,
                IsApproved = approveResult,
                NormativeDocumentId = visaNormativeDocumentId,
                Comment = visaComment
            };

            await _webApiClient.AddRecordAsync("NormativeDocumentVisas", visa, token);

            return RedirectToAction("Edit", new { @Id = visaNormativeDocumentId });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, Guid fileNormativeDocumentId)
        {
            try
            {
                string? token = HttpContext.Session.GetString("JWToken");
                var employee = (Employee?)HttpContext.Items["User"];

                if (employee == null || string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Index", "Login");
                }


                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();


                        var normativeDocumentFile = new NormativeDocumentFileAddDto
                        {
                            Id = Guid.NewGuid(),
                            FileName = file.FileName,
                            CreatedById = employee.Id,
                            CreatedOn = DateTime.Now,
                            Data = fileBytes,
                            NormativeDocumentId = fileNormativeDocumentId
                        };

                        await _webApiClient.AddRecordAsync("NormativeDocumentFiles", normativeDocumentFile, token);

                    }
                }

                return RedirectToAction("Edit", new { @Id = fileNormativeDocumentId });
            }
            catch
            {
                return RedirectToAction("Edit", new { @Id = fileNormativeDocumentId });
            }
        }

        private async Task<SelectList?> GetDocumentTypeItems()
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                var items = await _webApiClient.GetEntityRecordsAsync<IEnumerable<NormativeDocumentTypeGetDto>>("NormativeDocumentTypes", token);
                return new SelectList(items, nameof(NormativeDocumentTypeGetDto.Id), nameof(NormativeDocumentTypeGetDto.Name));

            }
            else
            {
                return null;
            }
        }

        private async Task<SelectList?> GetDocumentConfLevelItems()
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                var items = await _webApiClient.GetEntityRecordsAsync<IEnumerable<NormativeDocumentConfidentialityLevelGetDto>>("NormativeDocumentConfidentialityLevels", token);
                return new SelectList(items, nameof(NormativeDocumentConfidentialityLevelGetDto.Id), nameof(NormativeDocumentConfidentialityLevelGetDto.Name));

            }
            else
            {
                return null;
            }
        }

        private async Task<SelectList?> GetAvailableRelatedDocumentListAsync(Guid currentDocId)
        {
            string? token = HttpContext.Session.GetString("JWToken");
            var employee = (Employee?)HttpContext.Items["User"];

            if (!string.IsNullOrEmpty(token) && employee != null)
            {
                //var items = await _webApiClient.GetNormativeDocumentsByEmployeeIdAsync <IEnumerable<NormativeDocumentGetDto>>("NormativeDocuments", token);
                var items = await _webApiClient.GetNormativeDocumentsByEmployeeIdAsync(token, employee.Id);

                if (items != null)
                {
                    items = items.Where(x => x.Id != currentDocId);
                }

                return new SelectList(items, nameof(NormativeDocumentGetDto.Id), nameof(NormativeDocumentGetDto.DocumentNumber));

            }
            else
            {
                return null;
            }
        }

        private async Task<SelectList?> GetAvailableRelationTypesListAsync()
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                var items = await _webApiClient.GetEntityRecordsAsync<IEnumerable<NormativeDocumentRelationTypeGetDto>>("NormativeDocumentRelationTypes", token);
                return new SelectList(items, nameof(NormativeDocumentRelationTypeGetDto.Id), nameof(NormativeDocumentRelationTypeGetDto.Name));

            }
            else
            {
                return null;
            }
        }

        private async Task<IEnumerable<NormativeDocumentRelationGetWithDetailsDto>> GetRelatedDocumentsAsync(Guid id)
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                var items = await _webApiClient.GetEntityRecordsAsync<IEnumerable<NormativeDocumentRelationGetWithDetailsDto>>($"NormativeDocumentRelations/WithDetails/{id}", token);
                return items ?? new List<NormativeDocumentRelationGetWithDetailsDto>();
            }
            else
            {
                return new List<NormativeDocumentRelationGetWithDetailsDto>();
            }
        }
        private async Task<IEnumerable<NormativeDocumentVisaGetWithDetailsDto>> GetVisasAsync(Guid id)
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                var items = await _webApiClient.GetEntityRecordsAsync<IEnumerable<NormativeDocumentVisaGetWithDetailsDto>>($"NormativeDocumentVisas/WithDetails/{id}", token);
                return items ?? new List<NormativeDocumentVisaGetWithDetailsDto>();
            }
            else
            {
                return new List<NormativeDocumentVisaGetWithDetailsDto>();
            }
        }

        private async Task<IEnumerable<NormativeDocumentGetWithDetailsDto>> GetVersions(Guid id)
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                var items = await _webApiClient.GetEntityRecordsAsync<IEnumerable<NormativeDocumentGetWithDetailsDto>>($"NormativeDocuments/Versions/{id}", token);
                return items ?? new List<NormativeDocumentGetWithDetailsDto>();
            }
            else
            {
                return new List<NormativeDocumentGetWithDetailsDto>();
            }
        }

        private async Task<IEnumerable<NormativeDocumentFileGetWithDetailsDto>> GetFilesAsync(Guid id)
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                var items = await _webApiClient.GetEntityRecordsAsync<IEnumerable<NormativeDocumentFileGetWithDetailsDto>>($"NormativeDocumentFiles/WithDetails/{id}", token);
                return items ?? new List<NormativeDocumentFileGetWithDetailsDto>();
            }
            else
            {
                return new List<NormativeDocumentFileGetWithDetailsDto>();
            }
        }

        private async Task<int?> GetNextVersionAsync(Guid mainDocumentId)
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                return await _webApiClient.GetPrimitiveValueAsync<int>($"NormativeDocuments/MaxVersion/{mainDocumentId}", token);
            }
            else
            {
                return 1;
            }
        }




    }
}
