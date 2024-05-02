using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkTest
{
    internal class EFContext : DbContext
    {
        public EFContext() { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=University;TrustServerCertificate=True;Integrated Security=SSPI;", builder => builder.EnableRetryOnFailure());
        }
    }
}
