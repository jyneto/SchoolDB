using System;
using System.Collections.Generic;

namespace SchoolDB.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? FkStudentId { get; set; }

    public int? FkCourseId { get; set; }

    public int? FkEmployeeId { get; set; }

    public decimal? Grade1 { get; set; } 
    public DateTime? GradeDate { get; set; }

    public virtual Course? FkCourse { get; set; }

    public virtual Employee? FkEmployee { get; set; }

    public virtual Student? FkStudent { get; set; }
}
