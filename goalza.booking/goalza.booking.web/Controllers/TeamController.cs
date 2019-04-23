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
    [Authorize(Roles = "USER")]
    public class TeamController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICrudService<Team> _teamCrudService;
        private ApplicationUser user;

        public TeamController(
            ICrudService<Team> teamCrudService,
            UserManager<ApplicationUser> userManager)
        {
            _teamCrudService = teamCrudService;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/api/Team/Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                user = _userManager.GetUserAsync(HttpContext.User).Result;
                var record = await _teamCrudService.GetAsync(p => p.Id == user.TeamId);

                return Json(record);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(Team team)
        {
            try
            {
                user = _userManager.GetUserAsync(HttpContext.User).Result;
                Notification notification = new Notification();

                if (team.Id > 0)
                {
                    notification = await EditTeam(team, user);
                }
                else
                {
                    await _teamCrudService.InsertAsync(new Team
                    {
                        Name = team.Name,
                        Captain = user.UserName,
                        Manager = team.Manager,
                        PlayersCount = team.PlayersCount ++,
                        CreatedBy = user.UserName
                    });

                    notification.Type = "success";
                    notification.Message = "Team successfully initialized";
                }

                return Json(notification);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return Json(new Notification("error", "Team failed to initialize"));
            }
        }

        private async Task<Notification> EditTeam(Team team, ApplicationUser user)
        {
            try
            {
                var record = await _teamCrudService.GetAsync(team.Id);

                record.Name = team.Name;
                record.Captain = user.UserName;               
                record.ModifiedBy = user.UserName;
                record.ModifiedDate = DateTime.Now;

                _teamCrudService.Update(record);

                return new Notification("success", "Team successfully updated");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return new Notification("error", "Team failed to update.");
            }
        }
    }
}
