using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.Data.Db
{
    public abstract class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        public T CreateDbContext(string[] args)
        {
            string? designString = Environment.GetEnvironmentVariable("DESIGN_CONTEXT");

            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseSqlServer(designString);

            var dbContext = Activator.CreateInstance(
                typeof(T),
                optionsBuilder.Options) as T;

            return dbContext ?? throw new Exception($"Unable to create a DbContext of type {nameof(T)}");
        }
    }
}
