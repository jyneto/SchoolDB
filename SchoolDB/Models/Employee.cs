using System;
using System.Collections.Generic;

namespace SchoolDB.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Title { get; set; }

    public DateTime? HireDate { get; set; }
    public string? Contact { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
