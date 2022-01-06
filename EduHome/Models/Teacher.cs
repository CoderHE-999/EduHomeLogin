using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Desc { get; set; }
        public string Degree { get; set; }
        public string Hobbie { get; set; }
        public string Faculty { get; set; }
        [StringLength(maximumLength:100)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
