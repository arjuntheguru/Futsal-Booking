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
    [Authorize(Roles="ADMIN")]
    public class TimeSettingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICrudService<TimeSettings> _timeSettingsCrudService;
        private ApplicationUser user;

        public TimeSettingsController(
            UserManager<ApplicationUser> userManager,
            ICrudService<TimeSettings> timeSettingsCrudService)
        {
            _userManager = userManager;
            _timeSettingsCrudService = timeSettingsCrudService;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/api/TimeSettings/Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                user = _userManager.GetUserAsync(HttpContext.User).Result;
                var record = await _timeSettingsCrudService.GetAsync(p => p.FutsalCompanyId == user.FutsalCompanyId);

                return Json(record);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(TimeSettings timeSettings)
        {
            try
            {
                user = _userManager.GetUserAsync(HttpContext.User).Result;
                Notification notification = new Notification();

                if(timeSettings.Id > 0)
                {
                    notification = await EditTimeSettings(timeSettings, user);
                }
                else
                {
                    await _timeSettingsCrudService.InsertAsync(new TimeSettings
                    {
                        MorningStartTime = timeSettings.MorningStartTime,
                        MorningEndTime = timeSettings.MorningEndTime,
                        AfternoonStartTime = timeSettings.AfternoonStartTime,
                        AfternoonEndTime = timeSettings.AfternoonEndTime,
                        EveningStartTime = timeSettings.EveningStartTime,
                        EveningEndTime = timeSettings.EveningEndTime,
                        CreatedBy = user.UserName                        
                    });

                    notification.Type = "success";
                    notification.Message = "Time Settings successfully initialized";                   
                }

                return Json(notification);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                return Json(new Notification("error", "Time Settings failed to initialize"));
            }
        }

        private async Task<Notification> EditTimeSettings(TimeSettings timeSettings, ApplicationUser user)
        {
            try
            {
                var record = await _timeSettingsCrudService.GetAsync(timeSettings.Id);

                record.MorningStartTime = timeSettings.MorningStartTime;
                record.MorningEndTime = timeSettings.MorningEndTime;
                record.AfternoonStartTime = timeSettings.AfternoonStartTime;
                record.AfternoonEndTime = timeSettings.AfternoonEndTime;
                record.EveningStartTime = timeSettings.EveningStartTime;
                record.EveningEndTime = timeSettings.EveningEndTime;
                record.ModifiedBy = user.UserName;
                record.ModifiedDate = DateTime.Now;

                _timeSettingsCrudService.Update(record);

                return new Notification("success", "Time Settings successfully created");

            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                return new Notification("error", "Time Settings failed to update.");
            }
        }
    }
}
