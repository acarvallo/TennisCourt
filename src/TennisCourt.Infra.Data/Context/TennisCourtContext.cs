﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TennisCourt.Infra.CrossCutting.Commons.Providers;

namespace TennisCourt.Infra.Data.Context
{
    public class TennisCourtContext : DbContext
    {
        private readonly UserProvidedSettingsProvider _userProvided;
        public TennisCourtContext(DbContextOptions<TennisCourtContext> options,
                                   IOptions<UserProvidedSettingsProvider> userProvided)
           : base(options)
        {
            _userProvided = userProvided.Value;
        }
        public TennisCourtContext(DbContextOptions<TennisCourtContext> options)
            : base(options)
        {
            _userProvided = new UserProvidedSettingsProvider();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TennisCourtContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _userProvided.ConnectionString;
                optionsBuilder.UseSqlServer(connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                        sqlOptions.CommandTimeout(60);
                        sqlOptions.MigrationsAssembly("TennisCourt.Api");
                    });
                optionsBuilder.EnableSensitiveDataLogging(true);
            }

            base.OnConfiguring(optionsBuilder);
        }

    }
}