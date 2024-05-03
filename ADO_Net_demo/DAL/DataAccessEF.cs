using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Configuration;

namespace ADO_Net_demo.DAL
{
    internal class DataAccessEF : DbContext, IStudentsRepo
    {
        public DataAccessEF() : base()
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        public void Delete(int id)
        {
            var studentForDel = GetById(id);
            if (studentForDel != null)
            {
                Students.Remove(studentForDel);
                SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Student with that id doesnt exist");
            }
        }

        public Student GetById(int id)
        {
            var student = Students.
                Include(x => x.Courses).
                FirstOrDefault(x => x.StudentId == id);

            return student;
        }

        public List<Student> GetList()
        {
            List<Student> students = Students.
                                       Include(x => x.Courses).
                                       ToList();
            return students;
        }

        public Student Insert(Student student)
        {
            Students.Add(student);
            SaveChanges();
            return student;
        }

        public Student Update(Student student)
        {
            if (student.StudentId == 0)
            {
                throw new KeyNotFoundException("Student doesnt exist");
            }
            else
            {
                Students.Update(student);
                SaveChanges();
                return student;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable(x => x.HasTrigger("logTrigger"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString, builder => builder.EnableRetryOnFailure()).
                LogTo(Console.WriteLine, new[] {DbLoggerCategory.Database.Command.Name}, LogLevel.Information).
                EnableSensitiveDataLogging();
        }
    }
}
