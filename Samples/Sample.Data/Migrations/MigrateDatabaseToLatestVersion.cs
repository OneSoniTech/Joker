﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using Sample.Data.Context;

namespace Sample.Data.Migrations
{
  internal class MigrateDatabaseToLatestVersion : MigrateDatabaseToLatestVersion<SampleDbContext, Sample.Data.Migrations.Configuration>
  {    
    public MigrateDatabaseToLatestVersion(string connectionString)
      : base(connectionString)
    {
    }

    protected override void Seed(SampleDbContext context)
    {
      Configuration.ApplySeed(context);
    }
  }

  public class MigrateDatabaseToLatestVersion<TContext, TMigrationsConfiguration> : IDatabaseInitializer<TContext>
    where TContext : DbContext
    where TMigrationsConfiguration : DbMigrationsConfiguration<TContext>, new()
  {
    private readonly DbMigrationsConfiguration config;

    /// <summary>
    ///     Initializes a new instance of the MigrateDatabaseToLatestVersion class.
    /// </summary>
    public MigrateDatabaseToLatestVersion()
    {
      config = new TMigrationsConfiguration();
    }

    /// <summary>
    ///     Initializes a new instance of the MigrateDatabaseToLatestVersion class that will
    ///     use a specific connection string from the configuration file to connect to
    ///     the database to perform the migration.
    /// </summary>
    /// <param name="connectionString"> connection string to use for migration. </param>
    public MigrateDatabaseToLatestVersion(string connectionString)
    {
      config = new TMigrationsConfiguration
      {
        TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient")
      };
    }

    public void InitializeDatabase(TContext context)
    {
      if (context == null)
        throw new ArgumentException("Context passed to InitializeDatabase can not be null");

      var migrator = new DbMigrator(config);

      migrator.Update();

      Seed(context);
    }

    protected virtual void Seed(TContext context)
    {
    }
  }
}