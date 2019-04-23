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
    public class SocialMediaController : Controller
    {
        private readonly ICrudService<SocialMedia> _socialMediaCrudService;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser user;

        public SocialMediaController(
            ICrudService<SocialMedia> socialMediaCrudService,
            UserManager<ApplicationUser> userManager)
        {
            _socialMediaCrudService = socialMediaCrudService;
            _userManager = userManager;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/api/SocialMedia/Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                user = _userManager.GetUserAsync(HttpContext.User).Result;
                var record = await _socialMediaCrudService.GetAsync(p => p.FutsalCompanyId == user.FutsalCompanyId);

                return Json(record);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(SocialMedia socialMedia)
        {
            try
            {
                user = _userManager.GetUserAsync(HttpContext.User).Result;
                Notification notification = new Notification();

                if (socialMedia.Id > 0)
                {
                    notification = await EditSocialMedia(socialMedia, user);
                }
                else
                {
                    await _socialMediaCrudService.InsertAsync(new SocialMedia
                    {
                        Facebook = socialMedia.Facebook,
                        Instagram = socialMedia.Instagram,
                        Twitter = socialMedia.Twitter,
                        CreatedBy = user.UserName
                    });

                    notification.Type = "success";
                    notification.Message = "Social Media successfully initialized";
                }

                return Json(notification);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return Json(new Notification("error", "Social  failed to initialize"));
            }
        }

        private async Task<Notification> EditSocialMedia(SocialMedia socialMedia, ApplicationUser user)
        {
            try
            {
                var record = await _socialMediaCrudService.GetAsync(socialMedia.Id);

                record.Facebook = socialMedia.Facebook;
                record.Instagram = socialMedia.Instagram;
                record.Twitter = socialMedia.Twitter;              
                record.ModifiedBy = user.UserName;
                record.ModifiedDate = DateTime.Now;

                _socialMediaCrudService.Update(record);

                return new Notification("success", "Social Media successfully updated");
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                return new Notification("error", "Social Media failed to update.");
            }
        }
    }
}
