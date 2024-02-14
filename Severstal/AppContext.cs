using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Severstal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Severstal
{
    public class AppContext : DbContext
    {
        private readonly string _dataSource;
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Objective> Objectives { get; set; }

        public AppContext(string dataSourse = "Data Source=database.db") 
        {
            _dataSource = dataSourse;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnection(_dataSource);
            connectionStringBuilder.Open();
            optionsBuilder.UseSqlite(connectionStringBuilder);
        }
    }
}
