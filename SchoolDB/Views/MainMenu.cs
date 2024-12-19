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
        private readonly EmployeeManager _employeeManager;
        private readonly StudentManager _studentManager;
        private readonly GradeManager _gradeManager;

        public MainMenu(SchoolDBContext context)
        {
            _employeeManager = new EmployeeManager(context);
            _studentManager = new StudentManager(context);
            _gradeManager = new GradeManager(context);
        }

        public void ShowMainMemu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("1. View all employees");
                Console.WriteLine("2. View all students");
                Console.WriteLine("3  View students attending course");
                Console.WriteLine("4. View grades from the last month");
                Console.WriteLine("5. View course grades statistics");
                Console.WriteLine("6. Add new student");
                Console.WriteLine("7. Add new employee");
                Console.WriteLine("8. Exit");

                string choice = Console.ReadLine();

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
                        _gradeManager.ViewCourseGradeStats(); //Not working
                        break;
                    case "6":
                        _studentManager.AddStudents();
                        break;
                    case "7":
                        _employeeManager.AddEmployee();
                        break;
                    case "8":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }

            }
        }

    }
}
