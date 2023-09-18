using Microsoft.EntityFrameworkCore;

namespace EFCodeApproach.Models
{
    public class StudentDbContext:DbContext  // V.V.Imp: DbContext as Parent class for This class is Required as an approach to make relation with tables and database
    {
        public StudentDbContext(DbContextOptions options):base(options) // base is used to call a constructor of the base class.Options is passed as an argument to the base class constructor. This allows the base class (DbContext) to be initialized with the provided DbContextOptions.
        {
            
        }

        public DbSet<Student>Students { get; set; }  // This ensures that our Database Table will be named as Students only
    }
}
