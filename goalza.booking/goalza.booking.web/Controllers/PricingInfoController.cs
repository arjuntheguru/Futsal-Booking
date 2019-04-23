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
    [Authorize(Roles ="ADMIN")]
    public class PricingInfoController : Controller
    {
        private readonly ICrudService<PricingInfo> _pricingInfoCrudService;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser user;

        public PricingInfoController(
            ICrudService<PricingInfo> pricingInfoCrudService,
            UserManager<ApplicationUser> userManager)
        {
            _pricingInfoCrudService = pricingInfoCrudService;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/api/PricingInfo/Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                user = _userManager.GetUserAsync(HttpContext.User).Result;
                var record = await _pricingInfoCrudService.GetAsync(p => p.FutsalCourt.FutsalCompanyId == user.FutsalCompanyId);

                return Json(record);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(PricingInfo pricingInfo)
        {
            try
            {
                Notification notification = new Notification();
                user = _userManager.GetUserAsync(HttpContext.User).Result;

                if (pricingInfo.Id > 0)
                {
                    notification = await EditPricingInfo(pricingInfo, user);
                }
                else
                {
                    await _pricingInfoCrudService.InsertAsync(new PricingInfo
                    {
                        FutsalCourtId = pricingInfo.FutsalCourtId,
                        Morning = pricingInfo.Morning,
                        AfterNoon = pricingInfo.AfterNoon,
                        Evening = pricingInfo.Evening,
                        CreatedBy = user.UserName
                    });

                    notification.Type = "success";
                    notification.Message = "Pricing information successfully initialize";
                }

                return Json(notification);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return Json(new Notification("error", "Pricing information failed to intialize"));
            }
        }

        private async Task<Notification> EditPricingInfo(PricingInfo pricingInfo, ApplicationUser user)
        {
            try
            {
                var record = await _pricingInfoCrudService.GetAsync(pricingInfo.Id);

                record.Morning = pricingInfo.Morning;
                record.AfterNoon = pricingInfo.AfterNoon;
                record.Evening = pricingInfo.Evening;
                record.ModifiedBy = user.UserName;
                record.ModifiedDate = DateTime.Now;

                _pricingInfoCrudService.Update(record);

                return new Notification("success", "Pricing information successfully updated");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return new Notification("error", "Pricing information failed to update");
            }
        }
    }
}
