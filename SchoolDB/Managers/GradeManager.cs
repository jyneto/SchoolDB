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
        //Constructor
        public GradeManager(SchoolDBContext context)
        {
            _context = context;
        }

        //Method for 
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

        public void ViewCourseGradeStats()
        {
            var courses = _context.Courses.ToList();
            Console.Clear();
            Console.WriteLine("Courses retrieved: " + courses.Count);

            foreach (var course in courses)
            {
                Console.WriteLine($"\nProcessing course: {course.CourseName}");
                var grades = _context.Grades.Where(g => g.FkCourseId == course.CourseId).ToList();
                Console.WriteLine($"Grades found for course '{course.CourseName}': " + grades.Count);

                var gradeValues = grades.Select(g => g.Grade1).ToList();

                var gradeValueMap = new Dictionary<decimal, int>
                {
                    { 20m, 5}, // Changed from double to decimal
                    { 17.5m, 4},
                    { 15m, 3},
                    { 12.5m, 2},
                    { 10m, 1},
                    { 0m, 0}
                };

                var numericalGrades = gradeValues
                    .Select(g => g.HasValue && gradeValueMap.TryGetValue(g.Value, out int value) ? value : 0).ToList();
                Console.WriteLine($"Grades for course '{course.CourseName}': " + string.Join(", ", numericalGrades));

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
