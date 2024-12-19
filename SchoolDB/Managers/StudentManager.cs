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

        //Constructor
        public StudentManager(SchoolDBContext context)
        {
            _context = context;
        }
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

                if (sortName == "1" || sortName == "2")
                {
                    Console.WriteLine("FirstName LastName");
                    foreach (var student in students)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName}");
                    }
                }
                else
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"{student.LastName} {student.FirstName}");
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
        // Metod for view students in courses
        // Trying another way of showing menu alternative by using forloop and if statements
        public void ViewStudentsAttendingCourse()
        {
            // Displaying the list of students
            Console.WriteLine("***List of students***");
            var students = _context.Students.ToList();
            //students.ForEach(student => Console.WriteLine($"{student.StudentId} - {student.FirstName} {student.LastName} "));
            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentId} - {student.FirstName} {student.LastName}");
            }
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

                var studentsInCourse = _context.Enrollments
                                                .Where(e => e.FkCourseId == courseId)
                                                .Include(e => e.FkStudent)
                                                .Select(e => e.FkStudent)
                                                .ToList();

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

        //Method to add student 
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

                // Creating a new student out of a Student class
                var student = new Student()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = birthDate,
                    Contact = contact

                };
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
//[]  Hämta alla elever (ska lösas med Entity framework)
        
//                Användaren får välja om de vill se eleverna sorterade på för- eller efternamn och om det ska vara stigande eller fallande sortering.
        
// [ ] Hämta alla elever i en viss klass (ska lösas med Entity framework)
        
//                Användaren ska först få se en lista med alla klasser som finns, sedan får användaren välja en av klasserna och då skrivs alla elever i den klassen ut.
        
//                🏆 Extra utmaning (Frivillig): låt användaren även få välja sortering på eleverna som i punkten ovan.
