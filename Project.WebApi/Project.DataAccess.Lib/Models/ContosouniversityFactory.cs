using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Project.DataAccess.Lib.Models
{
    public class ContosouniversityFactory : IDesignTimeDbContextFactory<ContosouniversityContext>
    {
        public ContosouniversityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContosouniversityContext>();
            optionsBuilder.UseSqlServer("Data Source=ContosoUniversity;Initial Catalog=ContosoUniversity;Integrated Security=True");

            return new ContosouniversityContext(optionsBuilder.Options);
        }
    }
}
