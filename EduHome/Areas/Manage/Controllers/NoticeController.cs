using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class NoticeController : Controller
    {
        private readonly DataContext _context;

        public NoticeController(DataContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Notice.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Notice notice)
        {
            _context.Notice.Add(notice);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Notice notice = _context.Notice.FirstOrDefault(x => x.Id == id);
            if (notice == null) return NotFound();
            return View(notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Notice notice)
        {
            Notice existNotice = _context.Notice.FirstOrDefault(x => x.Id == notice.Id);

            if (existNotice == null) return NotFound();
            if (!ModelState.IsValid) return View();
            existNotice.Date = notice.Date;
            existNotice.Desc = notice.Desc;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Notice notice = _context.Notice.FirstOrDefault(x => x.Id == id);
            if (notice == null) return NotFound();

            return View(notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(Notice notice){
            Notice notices = _context.Notice.FirstOrDefault(x => x.Id == notice.Id);
            if (notices == null) return NotFound();

            _context.Remove(notices);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
