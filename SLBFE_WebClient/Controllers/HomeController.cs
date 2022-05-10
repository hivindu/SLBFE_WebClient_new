using Microsoft.AspNetCore.Mvc;
using SLBFE_WebClient.Models;
using SLBFE_WebClient.Services.Interfaces;

namespace SLBFE_WebClient.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ISLBFE_Services _service;

        public HomeController(ISLBFE_Services service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(LoginDetails collection)
        {
            try
            {
                var result = await _service.ValidateUserCredentials(collection);
                WriteCookie("User_TL_Id", result.Id);
                WriteCookie("User_TL_Name", result.Name);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("GetAllJobSeekersAsync")]
        public IActionResult GetAllJobSeekersAsync()
        {
            try
            {
                var result = _service.GetAllCitizens();
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("GetAllCompanies")]
        public IActionResult GetAllCompanies()
        {
            try
            {
                var result = _service.GetAllCitizens();
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var result = _service.GetAllUsers();
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(CreateUserRequest request)
        {
            try
            {
                var result = _service.CreateUserAccount(request);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser(string userId)
        {
            try
            {
                var result = _service.DeleteUser(userId);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("ChangeCompanyActivationStatus")]
        public IActionResult ChangeCompanyActivationStatus(string companyId, bool status)
        {
            try
            {
                var result = _service.ChangeCompanyActivationStatus(companyId, status);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("ChangeActivationStatus")]
        public IActionResult ChangeActivationStatus(string citizenId, bool status)
        {
            try
            {
                var result = _service.ChangeCitizenActivationStatus(citizenId, status);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("GetDetailedCitizenPerApplication")]
        public IActionResult GetDetailedCitizenPerApplication(string vacancyId)
        {
            try
            {
                var result = _service.GetDetailCitizensPerVacancy(vacancyId);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("UpdateVacancy")]
        public IActionResult UpdateVacancy(UpdateVacancyRequest request)
        {
            try
            {
                var result = _service.UpdateVacancy(request);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("DeleteVacancy")]
        public IActionResult DeleteVacancy(string vacancyId)
        {
            try
            {
                var result = _service.DeleteVacancy(vacancyId);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("CreateJobVacancy")]
        public IActionResult CreateJobVacancy(CreateVacancyRequest request)
        {
            try
            {
                var result = _service.CreateVacancy(request);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("CreateCompanyProfile")]
        public IActionResult CreateCompanyProfile(CreateCompanyUserRequest request)
        {
            try
            {
                var result = _service.CreateCompanyAccount(request);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("GetVacanciesPerCompany")]
        public IActionResult GetVacanciesPerCompany(string companyId)
        {
            try
            {
                var result = _service.GetVacanciesByCompany(companyId);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("ReplyToComplaint")]
        public IActionResult ReplyToComplaint(string complaintId, string replyMessage)
        {
            try
            {
                var result = _service.ReplyOnComplaint(complaintId, replyMessage);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("GetAllComplaints")]
        public IActionResult GetAllComplaints()
        {
            try
            {
                var result = _service.GetAllComplaints();
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("GetComplaintByCitizenId")]
        public IActionResult GetComplaintByCitizenId()
        {
            try
            {
                var result = _service.GetAllComplaints();
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("GetCitizensDetailsById")]
        public IActionResult GetCitizensDetailsById(string citizenId)
        {
            try
            {
                var result = _service.GetCitizensFullDetailsByCitizenId(citizenId);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("GetAllPendingVacancies")]
        public IActionResult GetAllPendingVacancies()
        {
            try
            {
                var result = _service.GetApprovalPendingVacanciesList();
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("ApproveVacancy")]
        public IActionResult ApproveVacancy(string vacancyId)
        {
            try
            {
                var result = _service.ApproveVacancy(vacancyId);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("GetCitizenById")]
        public IActionResult GetCitizenById(string userId)
        {
            try
            {
                var result = _service.GetCitizenById(userId);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost("VerifyCitizen")]
        public IActionResult VerifyCitizen(string citizenNic)
        {
            try
            {
                var result = _service.VerifyCitizen(citizenNic);
                return Json(result);
            }
            catch
            {
                return Json(false);
            }
        }

        public IActionResult WriteCookie(string key, string value)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(15);
            //options.HttpOnly = true;
            //options.Secure = true;
            HttpContext.Response.Cookies.Append(key, value, options);

            return View("WriteCookie");
        }
    }
}
