using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ako_api.Models
{
    public partial class AkoContext : DbContext
    {
        public AkoContext()
        {
        }

        public AkoContext(DbContextOptions<AkoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CoursePrerequisite> CoursePrerequisite { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<SubjectHeirarchy> SubjectHeirarchy { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCoursePlanner> UserCoursePlanner { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:ako.database.windows.net,1433;Initial Catalog=akodb;Persist Security Info=False;User ID=atea;Password=Master123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Message).IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Course_Comment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Comment");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_Subject_Course");
            });

            modelBuilder.Entity<CoursePrerequisite>(entity =>
            {
                entity.HasKey(e => e.PrerequisiteId)
                    .HasName("PK__CoursePr__25A953D910AEAE68");

                entity.HasOne(d => d.MainCourse)
                    .WithMany(p => p.CoursePrerequisiteMainCourse)
                    .HasForeignKey(d => d.MainCourseId)
                    .HasConstraintName("FK_Main_Course_CoursePrequisite");

                entity.HasOne(d => d.PrerequisiteCourse)
                    .WithMany(p => p.CoursePrerequisitePrerequisiteCourse)
                    .HasForeignKey(d => d.PrerequisiteCourseId)
                    .HasConstraintName("FK_Prerequsite_Course_CoursePrequisite");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<SubjectHeirarchy>(entity =>
            {
                entity.HasOne(d => d.ChildSubject)
                    .WithMany(p => p.SubjectHeirarchyChildSubject)
                    .HasForeignKey(d => d.ChildSubjectId)
                    .HasConstraintName("FK_Child_Subject_SubjectHeirarchy");

                entity.HasOne(d => d.ParentSubject)
                    .WithMany(p => p.SubjectHeirarchyParentSubject)
                    .HasForeignKey(d => d.ParentSubjectId)
                    .HasConstraintName("FK_Parent_Subject_SubjectHeirarchy");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.AuthProviderId).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);
            });

            modelBuilder.Entity<UserCoursePlanner>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.UserCoursePlanner)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Course_UserCoursePlanner");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCoursePlanner)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_UserCoursePlanner");
            });
        }
    }
}
