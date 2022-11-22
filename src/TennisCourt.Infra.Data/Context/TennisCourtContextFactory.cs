using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TennisCourt.Infra.Data.Context
{
    public class TennisCourtContextFactory : IDesignTimeDbContextFactory<TennisCourtContext>
    {
        public TennisCourtContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TennisCourtContext>();
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=tennis-court-api; User Id=sa; Password=appTennisCourt!1");
            return new TennisCourtContext(optionsBuilder.Options);
        }
    }
}