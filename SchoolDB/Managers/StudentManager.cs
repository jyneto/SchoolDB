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

        // Constructor to initialize the context
        public StudentManager(SchoolDBContext context)
        {
            _context = context;
        }

        // Method to view students with sorting options
        public void ViewStudents()
        {
            try
            {
                Console.WriteLine("[1] First name Ascending \n" +
                                  "[2] First name Descending \n" +
                                  "[3] Last name Ascending \n" +
                                  "[4] Last name Descending");

                string sortName = Console.ReadLine();
                List<Student> students = new List<Student>();

                // Sorting students based on user input
                switch (sortName)
                {
                    case "1":
                        students = _context.Students.OrderBy(s => s.FirstName).ToList();
                        break;
                    case "2":
                        students = _context.Students.OrderByDescending(s => s.FirstName).ToList();
                        break;
                    case "3":
                        students = _context.Students.OrderBy(s => s.LastName).ToList();
                        break;
                    case "4":
                        students = _context.Students.OrderByDescending(s => s.LastName).ToList();
                        break;
                }

                // Displaying sorted students
                if (sortName == "1" || sortName == "2")
                {
                    Console.WriteLine("First name : Last name");
                    foreach (var student in students)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName}");
                    }
                }
                else
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine("Last name : First name");
                        Console.WriteLine($"{student.LastName} , {student.FirstName}");
                    }
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("Press any key return to menu");
            Console.ReadKey();
        }

        // Method to view students attending a specific course
        public void ViewStudentsAttendingCourse()
        {
            // Displaying the list of students
            Console.WriteLine("***List of students***");
            var students = _context.Students.ToList();
            students.ForEach(student => Console.WriteLine($"{student.StudentId} - {student.FirstName} {student.LastName} "));

            // Displaying the list of courses
            Console.WriteLine("\n***View students by course***");
            Console.WriteLine("Type in course number to select ");

            var courses = _context.Courses.ToList();

            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {courses[i].CourseName}");
            }

            string input = Console.ReadLine();
            Console.Clear();
            if (int.TryParse(input, out int courseIndex) && courseIndex > 0 && courseIndex <= courses.Count)
            {
                int courseId = courses[courseIndex - 1].CourseId;

                // Fetching students enrolled in the selected course
                var studentsInCourse = _context.Enrollments
                                                .Where(e => e.FkCourseId == courseId)
                                                .Include(e => e.FkStudent)
                                                .Select(e => e.FkStudent)
                                                .ToList();

                // Displaying students in the selected course
                Console.WriteLine("Students in the selected course:");
                studentsInCourse.ForEach(student => Console.WriteLine($"{student.FirstName} {student.LastName}"));
            }
            else
            {
                Console.WriteLine("Invalid format. Please try again.");
            }

            Console.WriteLine("Press any key to choose another course return to main menu.");
            Console.ReadKey();
        }

        // Method to add a new student
        public void AddStudents()
        {
            try
            {
                Console.WriteLine("Enter student first name: ");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter student last name: ");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter employee title (ex. HR,Teacher etc): ");
                string title = Console.ReadLine();
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("Press any key return to menu");
            Console.ReadKey();
        }
    }
}
