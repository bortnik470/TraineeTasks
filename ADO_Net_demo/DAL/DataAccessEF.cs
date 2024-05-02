using Microsoft.EntityFrameworkCore;
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
            var studentForDel = Students.Where(x => x.StudentId == id).
                                         FirstOrDefault();
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
            return Students.
                Include(x => x.Courses).
                First(x => x.StudentId == id);
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
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString, builder => builder.EnableRetryOnFailure());
        }
    }
}
