using System;
using System.Collections.Generic;

namespace SchoolDB.Models;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public int? FkEmployeeId { get; set; }

    public int? FkCourseId { get; set; }

    public virtual Course? FkCourse { get; set; }

    public virtual Employee? FkEmployee { get; set; }
}
