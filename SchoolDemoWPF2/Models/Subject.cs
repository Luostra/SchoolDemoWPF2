using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDemoWPF2.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Teacher> Teachers { get; set; } = new();
        public List<Grade> Grades { get; set; } = new();
    }
}
