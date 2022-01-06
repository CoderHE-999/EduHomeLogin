using EduHome.Helper;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeacherController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public TeacherController(DataContext context , IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Teachers.ToList());
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Teacher teacher)
        {
            if (teacher.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "IMage is required");
            }

            else if (teacher.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Max size 2MB");
            }

            else if (teacher.ImageFile.ContentType != "image/jpeg" && teacher.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "Format is incorrect!(image.jpg or image.png)");
            }

            if (!ModelState.IsValid) return View();

            string fileName = teacher.ImageFile.FileName;
            fileName = fileName.Length < 64 ? fileName : (fileName.Substring(fileName.Length - 64, 64));
            fileName = Guid.NewGuid().ToString() + fileName;

            string path = Path.Combine(_env.WebRootPath, "Uploads/Teachers", fileName);

            using (FileStream stream = new FileStream(path , FileMode.Create))
            {
                teacher.ImageFile.CopyTo(stream);
            }

            teacher.Image = fileName;

            _context.Teachers.Add(teacher);
            _context.SaveChanges();

            return RedirectToAction("index");


        }

        public IActionResult Edit(int id)
        {
            Teacher teacher = _context.Teachers.Where(x => x.Id == id).FirstOrDefault();

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Teacher teacher)
        {
            Teacher teachers = _context.Teachers.FirstOrDefault(x => x.Id == teacher.Id);

            if (teachers == null) return NotFound();

            


            teachers.Title = teacher.Title;
            teachers.Position = teacher.Position;
            teachers.Hobbie = teacher.Hobbie;
            teachers.Faculty = teacher.Faculty;
            teachers.Desc = teacher.Desc;
            teachers.Degree = teacher.Degree;


              

            

            if (teacher.ImageFile != null)
            {
                FileManager.Delete(_env.WebRootPath, "Uploads/Teachers", teachers.Image);

                teachers.Image = FileManager.Save(_env.WebRootPath, "Uploads/Teachers", teacher.ImageFile);
            }

            else if (teacher.Image == null && teachers.Image != null)
            {
                FileManager.Delete(_env.WebRootPath, "Uploads/Teachers", teachers.Image);
                teachers.Image = null;

            }





            _context.SaveChanges();
            return RedirectToAction("index");



        }

        


    }
}
