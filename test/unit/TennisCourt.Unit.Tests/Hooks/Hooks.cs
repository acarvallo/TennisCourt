using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using TechTalk.SpecFlow;
using TennisCourt.Infra.Data.Context;
using TennisCourt.Unit.Tests.Infra;

namespace TennisCourt.Unit.Tests.Hooks
{
    [Binding]
    public sealed class Hooks
    {

        private static DbFixture _dbFixture;
        public static HttpClient _client;
        [BeforeTestRun]
        public static void BeforeTestRun()
        {

            _dbFixture = DbFixture.Create();
            _client = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(srv =>
            {
                var descriptor = srv
                .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TennisCourtContext>));

                if (descriptor is object)
                    srv.Remove(descriptor);

                srv.AddDbContext<TennisCourtContext>(opt => opt.UseSqlServer(_dbFixture.ConnectionString));
            }))
           .CreateClient();

        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            if (_dbFixture != null)
                _dbFixture.Dispose();
        }
    }
}
