using Microsoft.EntityFrameworkCore;
using SchoolDB.Data;
using SchoolDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SchoolDB.Managers
{
  
public class StudentManager
    {
        private readonly SchoolDBContext _context;
        private readonly GradeManager _gradeManager;
        // Constructor to initialize the context
        public StudentManager(SchoolDBContext context)
        {
            _context = context;
            _gradeManager = new GradeManager(context);
        }

        // Method to view students with sorting options
        public void ViewStudents()
        {

            Console.WriteLine("[1] Active Courses\n" +
                              "[2] Student Information \n" +
                              "[3] Grades from lastmonth\n" +
                              "[4] Create new student \n"
                              //"[4] Last name Descending"
                              );

            Console.WriteLine("Enter choice (1-4) OR press Enter to return to main menu.");
            
            string sortName = Console.ReadLine();
            List<Student> students = new List<Student>();

            // Sorting students based on user input
            switch (sortName)
            {
                case "1":
                    ViewActiveCourses();
                    break;
                case "2":
                    ViewStudentsAttendingCourse();
                    break;
                case "3":
                    _gradeManager.ViewGradesLastMonth();
                    break;
                case "4":
                   
                    AddStudents();
                    break;
                default:
                    Console.WriteLine("Enter valid number");
                    return;
            }
      
        }
            
        // Method to view students attending a specific course
        public void ViewStudentsAttendingCourse()
        {
            //Fetch all students along with their courses and grades
            var studentInCourse = _context.Enrollments
                                           .Include(e => e.FkStudent)
                                           .Include(e => e.FkCourse)
                                           .Include(e => e.FkStudent.Grades)
                                           .ToList();

            var students= studentInCourse
                                 .GroupBy(s => s.FkStudent.StudentId)
                                 .Select(g => new
                                 {
                                     Student = g.First().FkStudent,
                                     Course = g.Select(e => e.FkCourse).Distinct(),
                                     Grades = g.First().FkStudent.Grades
                                 })
                                 .OrderBy(s => s.Student.FirstName)
                                 .ThenBy(s => s.Student.LastName)
                                 .ToList();

            Console.WriteLine("Students attending courses:");
            foreach (var studentRecord in students)
            {
                var student = studentRecord.Student;

                Console.WriteLine();
                Console.WriteLine($"Student ID: {student.StudentId} - {student.FirstName} {student.LastName}");
                Console.WriteLine($"Birthdate: {student.BirthDate?.ToString("yyyy-MM-dd")}");
                Console.WriteLine($"Contact: {student.Contact}");

                //var studentCourses = studentInCourse
                //                        .Where(s => s.Student.StudentId == student.StudentId)
                //                        .Select(s => s.Course)
                //                        .Distinct();
                Console.WriteLine("Courses enrolled");
                foreach (var course in studentRecord.Course)
                {
                    Console.WriteLine($"    - {course.CourseName}, Status: {course.Status}");
                }

                //var studentGrades = studentRecord.Grades;
                Console.WriteLine("Grades:");
                foreach (var grade in studentRecord.Grades)
                {
                    Console.WriteLine($"    - Grade: {grade.Grade1}, Date: {grade.GradeDate?.ToString("yyyy-MM-dd")}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("");
            Console.WriteLine("** Press any key to return to main menu **");
            Console.ReadKey();
        }

        public void ViewActiveCourses()
        {
            var activeCourses = _context.Courses
                                        .Where(c => c.Status == "Active")
                                        .OrderBy(c => c.CourseName)
                                        .ToList();

            foreach (var course in activeCourses)
            {
                Console.WriteLine($"Course: {course.CourseName} - Status: {course.Status}");
            }
            Console.WriteLine("** Press any key to return to main menu **");
            Console.ReadKey();

        }

        // Method to add a new student
        public void AddStudents()
        {
           
                Console.WriteLine("Enter student first name: ");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter student last name: ");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter student birthdate (yyyy-mm-dd): ");
                DateTime birthDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter student contact: ");
                string contact = Console.ReadLine();

                // Creating a new student object
                var student = new Student()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = birthDate,
                    Contact = contact
                };

                // Adding the new student to the context and saving changes
                _context.Students.Add(student);
                _context.SaveChanges();
                Console.WriteLine("Student Added!");
                Console.WriteLine("** Press any key return to menu **");
                Console.ReadKey();
            

        }
    }
}
