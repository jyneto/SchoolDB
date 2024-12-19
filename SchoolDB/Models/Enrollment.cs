using System;
using System.Collections.Generic;

namespace SchoolDB.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int? FkStudentId { get; set; }

    public int? FkCourseId { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    public virtual Course? FkCourse { get; set; }

    public virtual Student? FkStudent { get; set; }
}
