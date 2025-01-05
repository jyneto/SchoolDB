using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SchoolDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDB.Managers
{
    public class GradeManager
    {
        private readonly SchoolDBContext _context;

        // Constructor
        public GradeManager(SchoolDBContext context)
        {
            _context = context;
        }

        // Method to view grades from the last month
        public void ViewGradesLastMonth()
        {
            var lastMonth = DateTime.Now.AddMonths(-1);

            var grades = _context.Grades
                .Where(g => g.GradeDate >= lastMonth)
                .Select(g => new
                {
                    g.FkStudent.FirstName,
                    g.FkStudent.LastName,
                    g.FkCourse.CourseName,
                    g.Grade1,
                    g.GradeDate
                }).ToList();

            foreach (var grade in grades)
            {
                Console.WriteLine($"{grade.FirstName} {grade.LastName} - {grade.CourseName}: {grade.Grade1} {grade.GradeDate?.ToString("yyyy-MM-dd")}");
            }
            Console.WriteLine("\n**Press any key to return to menu**");
            Console.ReadKey();
        }

        // Method to view course grade statistics
        public void ViewCourseGradeStats()
        {
            var courses = _context.Courses.ToList();
            Console.Clear();
            Console.WriteLine("Courses retrieved: " + courses.Count);

            foreach (var course in courses)
            {
                Console.WriteLine($"\nProcessing course: {course.CourseName}");

                var grades = _context.Grades
                    .Where(g => g.FkCourseId == course.CourseId)
                    .OrderBy(g => g.Grade1)
                    .ToList();
                Console.WriteLine($"Grades found for course '{course.CourseName}': " + grades.Count);

                var gradeValues = grades.Select(g => g.Grade1).ToList();

                if (gradeValues.Count > 0)
                {
                    var avgGrade = gradeValues.Average(g => g ?? 0);
                    var maxGrade = gradeValues.Max(g => g ?? 0);
                    var minGrade = gradeValues.Min(g => g ?? 0);

                    Console.WriteLine($"{course.CourseName} - Avg: {avgGrade:F2}, Max: {maxGrade}, Min: {minGrade}\n");
                }
                else
                {
                    Console.WriteLine($"{course.CourseName} has no grades.");
                }
            }
            Console.WriteLine("***Press any key to return to menu***");
            Console.ReadKey();
        }
    }
}
