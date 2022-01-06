using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class CourseItemViewModel
    {
        public int CourseId { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
    }
}
