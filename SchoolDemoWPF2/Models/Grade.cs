using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDemoWPF2.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; } // 2-5

        [Column(TypeName = "INTEGER")]
        public Semester Semester { get; set; } // Новое поле для семестра

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}

public enum Semester
{
    First = 1,  // 1 семестр
    Second = 2  // 2 семестр
}