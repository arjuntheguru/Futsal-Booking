using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goalza.booking.web.Data;
using goalza.booking.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace goalza.booking.web.Controllers
{
    [Authorize(Roles = "SUPER_ADMIN")]
    public class AdminController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AdminController> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/api/Admin/Get/{id?}")]
        public async Task<IActionResult> GetAdmin(string id)
        {
            try
            {
                if (id is null)
                {
                    var result = _userManager.GetUsersInRoleAsync("ADMIN").Result;
                    return Json(result);
                }
                else
                {
                    var result = await _userManager.FindByIdAsync(id);
                    return Json(result);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        [HttpGet]
        public JsonResult CheckUsername(string username, string userId)
        {
            var user = _userManager.FindByNameAsync(username).Result;
            return user == null ? Json(1) : user.Id == userId ? Json(1) : Json(0);
        }

        [HttpGet]
        public JsonResult CheckPhoneNo(string phoneNo, string userId)
        {
            var user = _context.Users.Where(x => x.PhoneNumber == phoneNo).SingleOrDefault();
            return user == null ? Json(1) : user.Id == userId ? Json(1) : Json(0);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ApplicationUser applicationUser, string password)
        {

            try
            {
                Notification notification = new Notification();
                if (applicationUser.Id == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = applicationUser.UserName,
                        Email = applicationUser.Email,
                        PhoneNumber = applicationUser.PhoneNumber,
                        Address = applicationUser.Address,
                        FutsalCompanyId = applicationUser.FutsalCompanyId
                    };
                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Admin created with a new account with password.");
                        await _userManager.AddToRoleAsync(user, "ADMIN");
                        notification.Type = "success";
                        notification.Message = "Admin created successfully.";
                    }
                    else
                    {
                        notification.Type = "error";
                        notification.Message = "Error while creating an Admin.";
                    }

                }
                else
                {
                    var user = await _userManager.FindByIdAsync(applicationUser.Id);


                    user.UserName = applicationUser.UserName;
                    user.PhoneNumber = applicationUser.PhoneNumber;
                    user.Email = applicationUser.Email;
                    user.Address = applicationUser.Address;
                   
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        notification.Type = "success";
                        notification.Message = "Admin updated successfully.";
                    }
                    else
                    {
                        notification.Type = "error";
                        notification.Message = "Error while updating the Admin.";
                    }
                }

                return Json(notification);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }
    }

}
