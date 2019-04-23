using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goalza.booking.core.Entity;
using goalza.booking.core.IRepository;
using goalza.booking.infrastructure;
using goalza.booking.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace goalza.booking.web.Controllers
{
    [Authorize(Roles="ADMIN")]
    public class FutsalCourtController : Controller
    {
        private readonly ICrudService<FutsalCourt> _futsalCourtCrudService;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser user;

        public FutsalCourtController(
            UserManager<ApplicationUser> userManager,
            ICrudService<FutsalCourt> futsalCourtCrudService)
        {
            _userManager = userManager;
            _futsalCourtCrudService = futsalCourtCrudService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/FutsalCourt/Get/{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            try
            {
                user = _userManager.GetUserAsync(HttpContext.User).Result;

                if (id is null)
                {
                   
                    var record = await _futsalCourtCrudService.GetAllAsync(p => p.FutsalCompanyId == user.FutsalCompanyId);
                    return Json(record);
                }
                else
                {
                    var record = await _futsalCourtCrudService.GetAsync(p => p.FutsalCompanyId == user.FutsalCompanyId && 
                    p.Id == id);
                    return Json(record);
                }
            }
            catch(Exception exception)
            {
                Console.Write(exception.Message);
                throw exception;                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(FutsalCourt futsalCourt)
        {
            try
            {
                Notification notification = new Notification();
                user = _userManager.GetUserAsync(HttpContext.User).Result;
                
                if(futsalCourt.Id > 0)
                {
                    notification = await EditFutsalCourt(futsalCourt, user);
                }
                else
                {
                    await _futsalCourtCrudService.InsertAsync(new FutsalCourt
                    {
                        Name = futsalCourt.Name,
                        Dimension = futsalCourt.Dimension,
                        Description = futsalCourt.Description,
                        CreatedBy = user.UserName                        
                    });

                    notification.Type = "success";
                    notification.Message = "Futsal Court successfully created";                    
                }

                return Json(notification);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                return Json(new Notification("error", "Futsal Court failed to create"));
            }
        }

        private async Task<Notification> EditFutsalCourt(FutsalCourt futsalCourt, ApplicationUser user)
        {
            try
            {
                var record = await _futsalCourtCrudService.GetAsync(futsalCourt.Id);

                record.Name = futsalCourt.Name;
                record.Dimension = futsalCourt.Dimension;
                record.Description = futsalCourt.Description;
                record.ModifiedBy = user.UserName;
                record.ModifiedDate = DateTime.Now;

                _futsalCourtCrudService.Update(record);

                return new Notification("success", "Futsal Court successfully updated");
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                return new Notification("error", "Futsal Court failed to update");
            }
        }
    }
}
