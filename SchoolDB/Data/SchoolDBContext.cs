using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SchoolDB.Models;

namespace SchoolDB.Data;

public partial class SchoolDBContext : DbContext
{
    public SchoolDBContext()
    {
    }

    //Dependency Injection
    public SchoolDBContext(DbContextOptions<SchoolDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
           => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__9E0E9F2FFB1D9FF6");

            entity.Property(e => e.AssignmentId).HasColumnName("Assignment_Id");
            entity.Property(e => e.FkCourseId).HasColumnName("Fk_course_id");
            entity.Property(e => e.FkEmployeeId).HasColumnName("Fk_employee_id");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK__Assignmen__Fk_co__47DBAE45");

            entity.HasOne(d => d.FkEmployee).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.FkEmployeeId)
                .HasConstraintName("FK__Assignmen__Fk_em__46E78A0C");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__37E005DBA76AC388");

            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.CourseName).HasMaxLength(200);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__781134A16B6D0D89");

            entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");
            entity.Property(e => e.Contact).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(70);
            entity.Property(e => e.LastName).HasMaxLength(70);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__4365BD4A8F29B8DC");

            entity.Property(e => e.EnrollmentId).HasColumnName("Enrollment_Id");
            entity.Property(e => e.FkCourseId).HasColumnName("Fk_CourseID");
            entity.Property(e => e.FkStudentId).HasColumnName("Fk_StudentID");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK__Enrollmen__Fk_Co__440B1D61");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK__Enrollmen__Enrol__4316F928");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__D4437133DF502732");

            entity.Property(e => e.GradeId).HasColumnName("Grade_Id");
            entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseID");
            entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Grade");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK__Grades__FK_Cours__3F466844");

            entity.HasOne(d => d.FkEmployee).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkEmployeeId)
                .HasConstraintName("FK__Grades__FK_Emplo__403A8C7D");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK__Grades__GradeDat__3E52440B");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__A2F4E98C5A1D3821");

            entity.Property(e => e.StudentId).HasColumnName("Student_Id");
            entity.Property(e => e.Contact).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(70);
            entity.Property(e => e.LastName).HasMaxLength(70);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
