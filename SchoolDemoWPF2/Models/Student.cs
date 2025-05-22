using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDemoWPF2.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public List<Grade> Grades { get; set; } = new();
    }
}
