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
                Console.WriteLine("*** MENU");
                Console.WriteLine("1. Employees");
                Console.WriteLine("2. Students");
                Console.WriteLine("3. Grades statistics");
                Console.WriteLine("4. Exit");

                // Read user choice
                string choice = Console.ReadLine();

                // Handle user choice
                switch (choice)
                {
                    case "1":
                        _employeeManager.ViewEmployeeOrDepartment();
                        break;
                    case "2":
                        _studentManager.ViewStudents();
                        break;
                    case "3":
                        _gradeManager.ViewCourseGradeStats();
                        break;
                    case "4":
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
