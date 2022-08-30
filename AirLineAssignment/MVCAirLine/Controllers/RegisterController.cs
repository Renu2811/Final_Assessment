using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAirLine.Data;
using MVCAirLine.Models;

namespace MVCAirLine.Controllers
{
    [Authorize(policy: "writepolicy")]
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Field> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterController(ApplicationDbContext applicationDbContext, UserManager<Field> userManager, RoleManager<IdentityRole> roleManager)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult RegisteredPage()
        {
            return View();
        }

       
        public IActionResult Index()
        {
            if (_applicationDbContext.AdminPage == null)
            {
                return NotFound();
            }
            List<AdminPage> admins = new List<AdminPage>();
            admins = _applicationDbContext.AdminPage.ToList();
            if (admins == null)
            {
                return NotFound();
            }
            return View(admins);
        }

        [HttpPost]
        public async Task<IActionResult> ForApproval(int id)
        {
            var data = _applicationDbContext.AdminPage.Find(id);
            if (data == null)
            {
                return NoContent();
            }

            var user = new Field()
            {
                Email = data.Email,
                PanNo = data.PanNo,
                UserName = data.Email
            };
            var roles = _roleManager.FindByNameAsync(data.RoleName).Result;
            await _userManager.CreateAsync(user, data.Password);
            await _userManager.AddToRoleAsync(user, roles.Name);
            _applicationDbContext.AdminPage.Remove(data);
            _applicationDbContext.SaveChanges();
            return Json("Success");
        }

       [HttpPost]
        public IActionResult rejected(int id)
        {
            var details = _applicationDbContext.AdminPage.Find(id);
            if (details == null)
            {
                return NotFound();
            }
            _applicationDbContext.AdminPage.Remove(details);
            _applicationDbContext.SaveChanges();
           
            return Json("Rejected");

        }
    }

}
