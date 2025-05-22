using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDemoWPF2.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? ClassroomId { get; set; }
        public Classroom Classroom { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public List<Grade> Grades { get; set; } = new();
    }
}
