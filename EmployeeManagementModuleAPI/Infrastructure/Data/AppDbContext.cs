using EmployeeManagementModuleAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementModuleAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Salary> Salaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Salary -> Employee (many-to-one)
            modelBuilder.Entity<Salary>()
            .HasOne(s => s.Employee)
            .WithMany()
            .HasForeignKey(s => s.EmpId)
            .OnDelete(DeleteBehavior.Cascade);

            // Employee -> Department (many-to-one)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DeptId)
                .OnDelete(DeleteBehavior.Restrict);
            //// Employee -> City (many-to-one)
            //modelBuilder.Entity<Employee>()
            //    .HasOne<City>()
            //    .WithMany()
            //    .HasForeignKey(e => e.CityId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
     .HasOne(e => e.City)
     .WithMany()
     .HasForeignKey(e => e.CityId)
     .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision for Salary.Amount
            modelBuilder.Entity<Salary>()
                .Property(s => s.Amount)
                .HasPrecision(18, 2);
            //  base.OnModelCreating(modelBuilder);
        }
    }
}
