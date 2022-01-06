using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string RedirectURL { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string BtnText { get; set; }

    }
}
