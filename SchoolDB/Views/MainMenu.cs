using SchoolDB.Data;
using SchoolDB.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDB.Views
{
    public class MainMenu
    {
        // Managers for handling employees, students, and grades
        private readonly EmployeeManager _employeeManager;
        private readonly StudentManager _studentManager;
        private readonly GradeManager _gradeManager;

        // Constructor initializing managers with the database context
        public MainMenu(SchoolDBContext context)
        {
            _employeeManager = new EmployeeManager(context);
            _studentManager = new StudentManager(context);
            _gradeManager = new GradeManager(context);
        }

        // Method to display the main menu and handle user input
        public void ShowMainMemu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("1. View all employees");
                Console.WriteLine("2. View all students");
                Console.WriteLine("3. View students attending course");
                Console.WriteLine("4. View grades from the last month");
                Console.WriteLine("5. View course grades statistics");
                Console.WriteLine("6. Add new student");
                Console.WriteLine("7. Add new employee");
                Console.WriteLine("8. Exit");

                // Read user choice
                string choice = Console.ReadLine();

                // Handle user choice
                switch (choice)
                {
                    case "1":
                        _employeeManager.ViewEmployees();
                        break;
                    case "2":
                        _studentManager.ViewStudents();
                        break;
                    case "3":
                        _studentManager.ViewStudentsAttendingCourse();
                        break;
                    case "4":
                        _gradeManager.ViewGradesLastMonth();
                        break;
                    case "5":
                        _gradeManager.ViewCourseGradeStats();
                        break;
                    case "6":
                        _studentManager.AddStudents();
                        break;
                    case "7":
                        _employeeManager.AddEmployee();
                        break;
                    case "8":
                        running = false; // Exit the loop and end the program
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
    }
}
