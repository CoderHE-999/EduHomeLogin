using EduHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class DashBoardController : Controller
    {
        private readonly UserManager<AppUser> _userManage;

        public DashBoardController(UserManager<AppUser> userManage)
        {
            this._userManage = userManage;
        }
        public IActionResult Index()
        {
            return View();
        }


        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new AppUser
        //    {
        //        UserName = "SuperAdmin",
        //        FullName = "Super admin"
        //    };

        //    var result = await _userManage.CreateAsync(admin, "Admin#12345");

        //    return Ok(result);
        //}
        

        
    }
}
