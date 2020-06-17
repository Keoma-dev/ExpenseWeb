using ExpenseWeb.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWeb.Database
{
    
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaidStatus>().HasData(
                new PaidStatus()
                {
                    Id = 1,
                    Name = "Nog niet betaald"
                },
                new PaidStatus()
                {
                    Id = 2,
                    Name = "Al betaald"
                });
            
        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<PaidStatus> PaidStatuses { get; set; }
    }
}
