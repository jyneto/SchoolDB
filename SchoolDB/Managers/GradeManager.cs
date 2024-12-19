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
                    g.GradeDate
                }).ToList();

            foreach (var grade in grades)
            {
                Console.WriteLine($"{grade.FirstName} {grade.LastName} - {grade.CourseName}: {grade.GradeDate?.ToString("yyyy-MM-dd")}");
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

            // Iterate through each course to process its grades
            foreach (var course in courses)
            {
                Console.WriteLine($"\nProcessing course: {course.CourseName}");

                // Retrieve all grades for the current course
                var grades = _context.Grades.Where(g => g.FkCourseId == course.CourseId).ToList();
                Console.WriteLine($"Grades found for course '{course.CourseName}': " + grades.Count);

                // Extract grade values from the grades
                var gradeValues = grades.Select(g => g.Grade1).ToList();

                // Map grade values to numerical equivalents
                var gradeValueMap = new Dictionary<decimal, int>
                    {
                        { 20m, 5},
                        { 17.5m, 4},
                        { 15m, 3},
                        { 12.5m, 2},
                        { 10m, 1},
                        { 0m, 0}
                    };

                // Convert grade values to numerical grades using the map
                var numericalGrades = gradeValues
                    .Select(g => g.HasValue && gradeValueMap.TryGetValue(g.Value, out int value) ? value : 0).ToList();
                Console.WriteLine($"Grades for course '{course.CourseName}': " + string.Join(", ", numericalGrades));

                // Calculate and display average, maximum, and minimum grades if there are any grades
                if (numericalGrades.Count > 0)
                {
                    var avgGrade = numericalGrades.Average();
                    var maxGrade = numericalGrades.Max();
                    var minGrade = numericalGrades.Min();

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
