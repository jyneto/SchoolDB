using SchoolDB.Data;
using SchoolDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDB.Managers
{
    // The EmployeeManager class provides methods to manage Employee entities in the SchoolDB database.
    // It includes methods to view and add employees, utilizing the SchoolDBContext for database operations.
    public class EmployeeManager
    {
        private readonly SchoolDBContext _context;

        // Constructor to initialize the EmployeeManager with a database context.
        public EmployeeManager(SchoolDBContext context)
        {
            _context = context;
        }


        List<Employee> employees = new List<Employee>();


        //Method to view employee or departments based on user input

        public void ViewEmployeeOrDepartment() // USE VIEW FROM DATABASE?
        {
            Console.WriteLine("Select category: ");
            Console.WriteLine("[1] All employees");
            Console.WriteLine("[2] Department");
            Console.WriteLine("[3] Number of employees per department");
            Console.WriteLine("[4] Create new employee");
            Console.WriteLine();
            Console.WriteLine("Enter your choice (1-3) OR press Enter to return to main menu.");
            Console.WriteLine();
            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    DisplayEmployeeList();
                    break;
                case "2":
                    ViewEmployees();
                    break;
                case "3":
                    ViewCountEmployeesPerDepartment();
                    break;
                case "4":
                    AddEmployee();
                    break;

            }
        }


        // Method to view employees based on their category.
        // Prompts the user to select a category and displays the corresponding employees.
        public void ViewEmployees()
        {
            Console.WriteLine("Select department:");
            Console.WriteLine("[1] Teacher");
            Console.WriteLine("[2] HR");
            Console.WriteLine("[3] Boss");
            Console.WriteLine("[4] School Counselor");
            Console.WriteLine("[5] Janitor");
            Console.WriteLine("[6] IT");
            Console.WriteLine("Enter your choice (1-6): ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    employees = _context.Employees.ToList().Where(e => e.Title.Equals("Teacher", StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "2":
                    employees = _context.Employees.ToList().Where(e => e.Title.Equals("HR", StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "3":
                    employees = _context.Employees.ToList().Where(e => e.Title.Equals("The Boss", StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "4":
                    employees = _context.Employees.ToList().Where(e => e.Title.Equals("School Counselor", StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "5":
                    employees = _context.Employees.ToList().Where(e => e.Title.Equals("Janitor", StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "6":
                    employees = _context.Employees.ToList().Where(e => e.Title.Equals("IT", StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option from the menu");
                    return;
            }

            DisplayEmployeeList();

        }

        //Method to display employee list
        public void DisplayEmployeeList()
        {
            var employees = _context.Employees
                                    .OrderBy(e => e.FirstName)
                                    .ThenBy(e => e.LastName)
                                    .ToList();

            if (employees.Count == 0)
            {
                Console.WriteLine($"No employees found for the given category.");
            }
            else
            {
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Title}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("** Press any key to return to main menu **");
            Console.ReadLine();
        }
        // Method to view departments and the number of employees in each department
        public void ViewCountEmployeesPerDepartment()
        {
            var departmentWithEmployeeCounts = _context.Employees
                                                       .GroupBy(e => e.Department)
                                                       .Select(g => new
                                                       {
                                                           DepartmentName = g.Key,
                                                           EmployeeCount = g.Count()
                                                       })
                                                       .ToList();
            Console.WriteLine("Numbers of employees per department:");
            Console.WriteLine();
            foreach (var department in departmentWithEmployeeCounts)
            {
                Console.WriteLine($"{department.DepartmentName}: {department.EmployeeCount} employee(s)");
            }
            Console.WriteLine();
            Console.WriteLine("** Press any key to return to main menu **");
            Console.ReadKey();
        }

        // Method to add a new employee to the database.
        // Prompts the user for employee details and saves the new employee to the database.
        public void AddEmployee()
        {
            Console.WriteLine("Enter employee first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter employee last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter employee title (ex. HR, Teacher, etc): ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter employee hire date (yyyy-mm-dd): ");
            DateTime hireDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter employee contact: ");
            string contact = Console.ReadLine();
            Console.WriteLine("Enter employee department");
            string department = Console.ReadLine();

            // Creating a new employee instance
            var employee = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                Title = title,
                HireDate = hireDate,
                Contact = contact,
                Department = department
            };

            // Adding the new employee to the context and saving changes to the database
            _context.Employees.Add(employee);
            _context.SaveChanges();
            Console.WriteLine("Employee created!");
            Console.WriteLine("** Press any key to return to main menu **");
            Console.ReadKey();
        }
    }
}
