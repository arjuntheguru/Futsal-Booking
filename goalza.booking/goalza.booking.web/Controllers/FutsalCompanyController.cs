using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goalza.booking.core.Entity;
using goalza.booking.core.IRepository;
using goalza.booking.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace goalza.booking.web.Controllers
{
    [Authorize]
    public class FutsalCompanyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICrudService<FutsalCompany> _futsalCompanyCrudService;        

        public FutsalCompanyController(
            UserManager<ApplicationUser> userManager,
            ICrudService<FutsalCompany> futsalCompanyCrudService)
        {
            _userManager = userManager;
            _futsalCompanyCrudService = futsalCompanyCrudService;            
        }

        // GET: /<controller>/
        [Authorize(Roles = "SUPER_ADMIN")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "SUPER_ADMIN")]
        [Route("/api/FutsalCompany/Get/{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            try
            {
                if (id is null)
                {
                    var result = await _futsalCompanyCrudService.GetAllAsync();
                    return Json(result);
                }
                else
                {
                    var result = await _futsalCompanyCrudService.GetAsync(id);
                    return Json(result);
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [Route("api/FutsalCompany/CurrentUser")]
        public async Task<IActionResult> CurrentUser()
        {
            try
            {
                var user = _userManager.GetUserAsync(HttpContext.User).Result;
                var result = await _futsalCompanyCrudService.GetAsync(user.FutsalCompanyId);

                return Json(result);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }


        [HttpPost]
        [Authorize(Roles = "SUPER_ADMIN")]
        public async Task<IActionResult> Save(FutsalCompany futsalCompany)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;

            try
            {
                Notification notification = new Notification();

                if (futsalCompany.Id > 0)
                {
               
                    notification = await EditFutsalCompany(futsalCompany,user);
                }
                else
                {
                    await _futsalCompanyCrudService.InsertAsync(new FutsalCompany
                    {
                        Name = futsalCompany.Name,
                        RegistrationNo = futsalCompany.RegistrationNo,
                        Address = futsalCompany.Address,
                        Email = futsalCompany.Email,
                        ContactNo = futsalCompany.ContactNo,
                        Description = futsalCompany.Description,
                        OpeningTime = futsalCompany.OpeningTime,
                        ClosingTime = futsalCompany.ClosingTime,
                        CreatedBy = user.UserName,
                        CreatedDate = DateTime.Now
                    });
                    notification.Type = "success";
                    notification.Message = "Futsal Company successfully created";                   
                }

                return Json(notification);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return Json(new Notification("error", "Futsal Company failed to create"));
            }
        }

        private async Task<Notification> EditFutsalCompany(FutsalCompany company, ApplicationUser user)
        {
            try
            {
                var record = await _futsalCompanyCrudService.GetAsync(company.Id);

                record.Name = company.Name;
                record.Email = company.Email;
                record.Address = company.Address;
                record.ContactNo = company.ContactNo;
                record.OpeningTime = company.OpeningTime;
                record.ClosingTime = company.ClosingTime;
                record.ContactNo = company.ContactNo;
                record.Description = company.Description;
                record.ModifiedBy = user.UserName;
                record.ModifiedDate = DateTime.Now;

                _futsalCompanyCrudService.Update(record);

                return new Notification("success", "Futsal Company successfully updated");

            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                return new Notification("error", "Futsal Company failed to update");
            }
        }
    }
}
