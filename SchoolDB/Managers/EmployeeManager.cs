﻿using SchoolDB.Data;
using SchoolDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDB.Managers
{
    public class EmployeeManager
    {
        private readonly SchoolDBContext _context;

        //Constructor
        public EmployeeManager(SchoolDBContext context)
        {
            _context = context;
        }

        //Method to view employees
        public void ViewEmployees()
        {
         
            Console.WriteLine("Select category of employees:");
            Console.WriteLine("1. Teacher");
            Console.WriteLine("2. HR");
            Console.WriteLine("3. Boss");
            Console.WriteLine("4. School Counselor");
            Console.WriteLine("5. Janitor");
            Console.WriteLine("6. IT");
            Console.WriteLine("7. View all employees");
            Console.WriteLine("Enter your choice (1-5): ");

            string choice = Console.ReadLine();
            Console.Clear();

            List<Employee> employees = new List<Employee>();

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
                case "7":
                    employees = _context.Employees.ToList();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option from the menu");
                    return;

            }
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found for the given category.");
            }
            else
            {
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Title}");
                }
            }
            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();

        }

        //Method to add Employees

        public void AddEmployee()
        {
            Console.WriteLine("Enter employee first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter employee last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter employee title (ex. HR,Teacher etc): ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter employee hire date (yyyy-mm-dd): ");
            DateTime hireDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter emplpyee contact: ");
            string contact = Console.ReadLine();

            // Creating a new employee out of a Employee class
            var employee = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                Title = title,
                HireDate = hireDate,
                Contact = contact

            };
            _context.Employees.Add(employee);// Build in method to inform EF context that we are trying to add a new employee into the DbSet connected to _context
            _context.SaveChanges();// Commit the changes that been made into the database
            Console.WriteLine("Employee Added! \n Press any key to return in menu");
            Console.ReadKey();
         
        }

    }
}
