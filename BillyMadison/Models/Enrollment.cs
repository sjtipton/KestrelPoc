using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillyMadison.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime? DroppedOn { get; set; }
    }
}
