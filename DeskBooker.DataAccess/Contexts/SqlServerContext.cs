using System;
using System.Collections.Generic;
using DeskBooker.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeskBooker.DataAccess.Contexts
{
    public class SqlServerContext : DbContext
    {
        public string ConnectionString { get; set; }

        public SqlServerContext() : base() { }

        public SqlServerContext(DbContextOptions<SqlServerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Department>().HasData(
            //     new Department { dept_no = 1, dept_name = "Dept 1" },
            //     new Department { dept_no = 2, dept_name = "Dept 2" }
            // );
        }
    }
}