using BillyMadison.Models;
using System.Linq;

namespace BillyMadison.Data
{
    public class DbInitializer
    {
        public static void Initialize(BillyMadisonContext context)
        {
            context.Database.EnsureCreated();

            // Initialize with students and courses; return if any students or courses already exist
            if (context.Students.Any() || context.Courses.Any()) return;

            // Students
            var students = new Student[]
            {
                new Student{FirstName="Jamie", LastName="Lannister", Phone="977-123-4567", Email="kingslayer@casterlyrock.net"},
                new Student{FirstName="Arya", LastName="Stark", Phone="434-555-1212", Email="noone@houseofblackandwhite.org"},
                new Student{FirstName="Sandor", LastName="Clegane", Phone="757-587-3258", Email="2chickens@cleganebowlconfirmed.net"},
                new Student{FirstName="Aegon", LastName="Targaryen", Phone="123-456-7890", Email="jonsnow@tptwp.org"},
                new Student{FirstName="Samwell", LastName="Tarly", Phone="444-344-7744", Email="healer@thecitadel.org"}
            };

            // Courses
            var courses = new Course[]
            {
                new Course{Name="Wizardry", CourseCode="W123", Description="Learn to do awesome wizard things"},
                new Course{Name="Sword Fighting with One Hand", CourseCode="KS222", Description="Learn to start over from scratch with a sword with your lesser used hand"},
                new Course{Name="Genealogy 101", CourseCode="G101", Description="Discovery your heritage........................."},
                new Course{Name="The Art of Becoming Another Person", CourseCode="FM000", Description="A man does not want to take such a course..."},
                new Course{Name="Cooking with Fire", CourseCode="KFC32", Description="Learn to cook your own damn chickens on an open flame, and get over fear at the same time!"}
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }

            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }

            context.SaveChanges();
        }
    }
}
