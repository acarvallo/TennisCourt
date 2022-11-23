using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourt.Infra.Data.Context;

namespace TennisCourt.Unit.Tests.Infra
{
    public class DbFixture : IDisposable
    {

        private bool disposed;
        private readonly TennisCourtContext dbContext;

        public string ConnectionString { get; }
        private DbFixture()
        {
            ConnectionString = $"Data Source=localhost; Initial Catalog=tennis-court-api-{Guid.NewGuid()}; User Id=sa; Password=appTennisCourt!1";

            var builder = new DbContextOptionsBuilder<TennisCourtContext>();

            builder.UseSqlServer(ConnectionString);
            dbContext = new TennisCourtContext(builder.Options);

            dbContext.Database.Migrate();
        }
        public static DbFixture Create() => new();
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Database.EnsureDeleted();
                    dbContext.Dispose();
                }

                disposed = true;
            }
        }
    }
}
