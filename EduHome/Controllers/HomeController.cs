using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {

        DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Courses = _context.Courses.ToList(),
                Notices = _context.Notice.ToList()
            };

            return View(homeVM);
        }

        public IActionResult Detail(int id)
        {
            Course course = _context.Courses.FirstOrDefault(x => x.Id == id);

            if (course == null) return NotFound();

            return PartialView(course);
        }

        public IActionResult AddBasket(int courseId)
        {
            if (!_context.Courses.Any(x => x.Id == courseId))
            {
                return NotFound();
            }


            List<CourseItemViewModel> basketItems = new List<CourseItemViewModel>();
            string existBasketItems = HttpContext.Request.Cookies["ItemList"];

            if (existBasketItems != null)
            {
                basketItems = JsonConvert.DeserializeObject<List<CourseItemViewModel>>(existBasketItems);
            }

            CourseItemViewModel item = basketItems.FirstOrDefault(x => x.CourseId == courseId);

            if (item == null)
            {
                item = new CourseItemViewModel
                {
                    CourseId = courseId,
                    Count = 1
                };
                basketItems.Add(item);
            }
            else
            {
                item.Count++;
            }


            var bookIdStr = JsonConvert.SerializeObject(basketItems);

            HttpContext.Response.Cookies.Append("ItemList", bookIdStr);

            return Ok();
        }


        public IActionResult ShowBasket()
        {
            var bookIdStr = HttpContext.Request.Cookies["ItemList"];
            List<CourseItemViewModel> bookIds = new List<CourseItemViewModel>();
            if (bookIdStr != null)
            {
                bookIds = JsonConvert.DeserializeObject<List<CourseItemViewModel>>(bookIdStr);
            }

            return Json(bookIds);
        }



    }
}
